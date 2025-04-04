using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model.FichaEpi;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;



namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FichaColaboradoresController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FichaColaboradoresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("BuscarColaboradoresPorMatricula")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Colaboradores encontrados com sucesso", Type = typeof(List<FichaColaboradorViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum colaborador encontrado", Type = typeof(List<FichaColaboradorViewModels>))]
        public async Task<IActionResult> BuscarColaboradoresPorMatricula(string matricula)
        {
            if (string.IsNullOrWhiteSpace(matricula) || !matricula.All(char.IsDigit))
            {
                return BadRequest(new
                {
                    message = "A matrícula deve conter apenas números e não pode estar vazia.",
                    status = 400
                });
            }

            try
            {
                var colaboradores = await _unitOfWork.ColaboradorServices.ObterColaboradoresAtivos();
                if (colaboradores == null || !colaboradores.Any())
                {
                    return NotFound(new
                    {
                        message = "Nenhum colaborador ativo encontrado.",
                        status = 404
                    });
                }

                var resultado = colaboradores
                    .Where(u => u.Matricula.ToString().StartsWith(matricula)) 
                    .Select(u => new FichaColaboradorViewModels
                    {
                       
                        NomeCompleto = u.NomeCompleto,
                        Matricula = u.Matricula,
                        ColaboradoresId = u.Id,

                    })
                    .ToList();

                if (!resultado.Any())
                {
                    return NotFound(new
                    {
                        message = $"Nenhum colaborador encontrado com matrícula iniciando com '{matricula}'.",
                        status = 404
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno: " + ex.Message,
                    status = 500
                });
            }
        }



        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Ficha cadastrado com sucesso", Type = typeof(FichaRegisterViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Usuário nao autenticado", Type = typeof(FichaRegisterViewModels))]
        public async Task<IActionResult> Register(FichaRegisterViewModels model)
        {
            try
            {
                var fichaexistente = await _unitOfWork.FichaColaboradorServices.ObterFichaPorColaboradorId(model.ColaboradoresId);
                if (fichaexistente != null)
                {
                    return NotFound("Já existe uma ficha cadastrada com esse colaborador.");
                }
                var fichaepis = new FichaColaborador
                {
                    
                    ColaboradoresId = model.ColaboradoresId,
                    DataCadastro = DateTime.Now.Date,
                    StatusFicha = StatusFichaEnum.Ativo

                };
                await _unitOfWork.FichaColaboradorServices.Cadastro(fichaepis);
                await _unitOfWork.FichaColaboradorServices.Salvar();
                return Ok(new
                {
                    message = $"Ficha cadastrado com sucesso.",
                    data = new
                    {
                        fichaepis.ColaboradoresId,
                        fichaepis.DataCadastro,
                        fichaepis.StatusFicha,


                    }
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        [HttpGet]
        [Route("BuscarFichaColaboradoresAtivos")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Ficha encontrado com sucesso", Type = typeof(FichaIndexViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Ficha não encontrado", Type = typeof(FichaIndexViewModels))]
        public async Task<IActionResult> BuscarFichaColaboradoresAtivos()
        {

            var fichaepi = await _unitOfWork.FichaColaboradorServices.ObterFichaEpiAtivo();
            if (fichaepi == null || !fichaepi.Any())
            {
                return NotFound();
            }
            var resultado = fichaepi.Select(u => new FichaIndexViewModels
            {
                Id = u.Id,
                NomeCompleto = u.Colaboradores.NomeCompleto,
                DataCadastro = u.DataCadastro,
                Matricula = u.Colaboradores.Matricula,
                StatusFicha = StatusFichaEnum.Ativo,



            }).ToList();

            return Ok(resultado);


        }

        [HttpGet]
        [Route("BuscarFichaColaborador")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Colaboradores encontrados com sucesso", Type = typeof(List<FichaBuscaViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum colaborador encontrado", Type = typeof(List<FichaBuscaViewModels>))]
        public async Task<IActionResult> BuscarColaboradoresPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest(new
                {
                    message = "O parâmetro de busca não pode estar vazio.",
                    status = 400
                });
            }

            try
            {
                var fichas = await _unitOfWork.FichaColaboradorServices.ObterFichaEpiAtivo();
                if (fichas == null || !fichas.Any())
                {
                    return NotFound(new
                    {
                        message = "Nenhuma ficha ativa foi encontrada.",
                        status = 404
                    });
                }

                var resultado = fichas
                    .Where(u =>
                        !string.IsNullOrWhiteSpace(u.Colaboradores.NomeCompleto) &&
                        u.Colaboradores.NomeCompleto.IndexOf(nome.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    .Select(u => new FichaBuscaViewModels
                    {
                        NomeCompleto = u.Colaboradores.NomeCompleto,
                        Matricula = u.Colaboradores.Matricula,
                        DataCadastro = u.DataCadastro,
                        StatusFicha = StatusFichaEnum.Ativo,
                    })
                    .ToList();

                if (!resultado.Any())
                {
                    return NotFound(new
                    {
                        message = $"Nenhuma ficha encontrada contendo o texto '{nome}' no nome.",
                        status = 404
                    });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro interno: " + ex.Message,
                    status = 500
                });
            }
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, description: "Ficha atualizada com sucesso", Type = typeof(FichaUpDateViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Ficha não encontrado", Type = typeof(FichaUpDateViewModels))]
        public async Task<IActionResult> Update(FichaUpDateViewModels model)
        {
            try
            {
                var fichaUpdate = await _unitOfWork.FichaColaboradorServices.ObterPorId(model.Id);
                if (fichaUpdate == null)
                {
                    return BadRequest(new
                    {
                        message = "Erro ao encontra essa ficha.",
                        status = 400
                    });
                }

                fichaUpdate.StatusFicha = model.StatusFicha;

                await _unitOfWork.FichaColaboradorServices.Atualizar(fichaUpdate);
                await _unitOfWork.FichaColaboradorServices.Salvar();

                return Ok($"Ficha inativado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

    }

}

