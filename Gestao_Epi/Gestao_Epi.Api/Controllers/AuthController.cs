using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model.Auth;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;

namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationJwtServices _authenticationJwtServices;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(SignInManager<User> signInManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            UserManager<User> userManager,
            IAuthenticationJwtServices authenticationJwtServices,
            IUnitOfWork unitOfWork
            )
        {
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
            _authenticationJwtServices = authenticationJwtServices;
            _unitOfWork = unitOfWork;
        }

        [Route("BuscarEstatisticasUsers")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, description: "Usuários encontrados com sucesso", Type = typeof(List<ValidarCampos>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum usuário encontrado", Type = typeof(List<ValidarCampos>))]
        public async Task<IActionResult> BuscarEstatisticasUsers()
        {
            var totalUsersAtivos = await _unitOfWork.UserServices.ObterTotalUsuarios();

            if (totalUsersAtivos == 0)
            {
                return NotFound("Nenhum usuários  encontrado.");
            }

            var resultado = new
            {
                TotalUsersAtivos = totalUsersAtivos,
            };

            return Ok(resultado);
        }


        [HttpPost]
        [Route("Register")]
        [SwaggerResponse(statusCode: 200, description: "Usuário cadastrado com sucesso", Type = typeof(UsersViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Usuário não autenticado", Type = typeof(UsersViewModels))]
        public async Task<IActionResult> Register(UsersViewModels model)
        {
            try
            {
                var matriculaExistente = await _userManager.FindByNameAsync(model.Matricula);
                if (matriculaExistente != null)
                {
                    return BadRequest(new
                    {
                        message = "A matrícula informada já está registrada no sistema.",
                        status = 400
                    });
                }

                var user = new User
                {
                    Matricula = model.Matricula,
                    NomeCompleto = model.NomeCompleto,
                    UserName = model.Matricula,
                    CreatedDate = DateTime.Now,
                    StatusUser = StatusUserEnum.Ativo
                };
                var passwordHasher = new PasswordHasher<User>();
                string senhaPadrao = "Senha@123"; 
                user.PasswordHash = passwordHasher.HashPassword(user, senhaPadrao);

                var resultado = await _userManager.CreateAsync(user, senhaPadrao);

                return Ok(new
                {
                    message = $"Usuário cadastrado com sucesso.",
                    data = new
                    {
                        user.Matricula,
                        user.CreatedDate,
                        user.NomeCompleto,
                        user.StatusUser
                    }
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Dados de login inválidos.");
                }

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return Unauthorized(new { message = "Usuário não encontrado!" });
                }

                if (await _userManager.IsLockedOutAsync(user))
                {
                    return Unauthorized(new { message = "Conta bloqueada. Tente novamente mais tarde." });
                }

                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordCheck)
                {
                    return Unauthorized(new { message = "Senha incorreta." });
                }

                await _userManager.ResetAccessFailedCountAsync(user);

                var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, new System.Security.Claims.ClaimsPrincipal(principal));

                var token = _authenticationJwtServices.CreateToken(user);
                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                return Ok(new
                {
                    token,
                    TokenExpiraEm = jsonToken?.ValidTo,
                    UserName = model.UserName,
                    Name = $"{user.NomeCompleto}"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao fazer login: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Update")]
        [SwaggerResponse(statusCode: 200, description: "Usuário atualizado com sucesso", Type = typeof(UpdateUserViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "usuário não encontrado", Type = typeof(UpdateUserViewModels))]
        public async Task<IActionResult> Update(UpdateUserViewModels model)
        {
            try
            {
                var user = await _unitOfWork.UserServices.ObterPorMatricula(model.Matricula);
                if (user == null)
                {
                    return BadRequest(new
                    {
                        message = "Erro ao encontra o usuário.",
                        status = 400
                    });
                }

                user.StatusUser = model.Status;


                await _unitOfWork.UserServices.Atualizar(user);
                await _unitOfWork.UserServices.Salvar();

                return Ok($"Usuário Inativado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("BuscarUsuariosAtivos")]
        [SwaggerResponse(statusCode: 200, description: "Usuário encontrado com sucesso", Type = typeof(UsuariosViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Colaborador não encontrado", Type = typeof(UsuariosViewModels))]
        public async Task<IActionResult> BuscarUsuariosAtivos()
        {

            var usuarios = await _unitOfWork.UserServices.ObterUsuariosAtivos();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound();
            }
            var resultado = usuarios.Select(u => new UsuariosViewModels
            {

                NomeCompleto = u.NomeCompleto,
                Matricula = u.Matricula,
                DataCadastro = u.CreatedDate,
                Status = StatusUserEnum.Ativo,

            }).ToList();

            return Ok(resultado);


        }


    }
}
