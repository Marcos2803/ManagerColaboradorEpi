using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model.Auth;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PermissoesController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public PermissoesController(SignInManager<User> signInManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("AtribuirRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AtribuirRoleAoUsuario([FromBody] UserPermissionViewModels model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null)
                {
                    return NotFound(new { message = "Role não encontrada." });
                }

                // Verifica se o usuário já possui a role
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains(role.Name))
                {
                    return BadRequest(new { message = "O usuário já possui essa role." });
                }

                // Adiciona a role ao usuário
                var result = await _userManager.AddToRoleAsync(user, role.Name);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "Erro ao adicionar role ao usuário.", errors = result.Errors });
                }

                return Ok(new { message = $"Role '{role.Name}' atribuída ao usuário '{user.UserName}' com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno.", error = ex.Message });
            }
        }

    }
}
