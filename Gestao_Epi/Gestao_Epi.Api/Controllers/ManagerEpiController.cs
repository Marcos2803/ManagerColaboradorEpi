using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model.FichaEpiItens;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerEpiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManagerEpiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("BuscarEpisItens")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epi não encontrados com sucesso", Type = typeof(List<BuscarEpisViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum epi encontrado", Type = typeof(List<BuscarEpisViewModels>))]
        public async Task<IActionResult> BuscarEpisItens(string nomeepi)
        {
            try
            {
                var buscaepis = await _unitOfWork.EpiServices.ObterEpisAtivos();
                
                if (buscaepis == null || !buscaepis.Any())
                {
                    return NotFound(new
                    {
                        message = "Nenhum epi ativo encontrado.",
                        status = 404
                    });
                }

                var resultado = buscaepis
                    .Where(u =>
                        !string.IsNullOrWhiteSpace(u.NomeEpi) &&
                        u.NomeEpi.IndexOf(nomeepi.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    .Select(u => new BuscarEpisViewModels
                    {

                        NomeEpi = u.NomeEpi,
                        EpisId = u.Id,
                    })
                    .ToList();

                if (!resultado.Any())
                {
                    return NotFound(new
                    {
                        message = $"Nenhum colaborador encontrado com nome iniciando com '{nomeepi}'.",
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
        [SwaggerResponse(statusCode: 200, description: "Epi cadastrado com sucesso", Type = typeof(FichaEpiItensViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Usuário nao autenticado", Type = typeof(FichaEpiItensViewModels))]
        public async Task<IActionResult> Register(FichaEpiItensViewModels model)
        {
            try
            {

                var epiExists = await _unitOfWork.EpiServices.ObterPorId(model.EpiId);
                if (epiExists == null)
                {
                    return BadRequest("O EPI informado não existe.");
                }

                var fichaExists = await _unitOfWork.FichaColaboradorServices.ObterPorId(model.FichaId);
                if (fichaExists == null)
                {
                    return BadRequest("A ficha informada não existe.");
                }
                var fichaepisitens = new FichaEpiItens
                {
                    Id = model.Id,
                    DataEntrega = DateTime.UtcNow.Date,
                    EpisId = model.EpiId,
                    AssinaturaEntrega = "Assinatura",
                    FichaId = model.FichaId,
                    ValidadeEpi = model.ValidadeEpi,
                    StatusFichaEpi = StatusFichaEpiEnum.Entregue,
                    DataDevolucao = null,
                    AssinaturaDevolucao = null

                };
                await _unitOfWork.FichaEpiItensServices.Cadastro(fichaepisitens);
                await _unitOfWork.FichaEpiItensServices.Salvar();
                return Ok($"Ficha epi cadastrada com sucesso");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObterEpisAVencerEmTresMeses")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epi proximo do encontrado encontrado", Type = typeof(List<ObterEpiAVenceViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhuma epi a vence  encontrado", Type = typeof(ObterEpiAVenceViewModels))]
        public async Task<IActionResult> ObterEpisAVencerEmTresMeses()
        {
            try
            {
                var episAVence = await _unitOfWork.FichaEpiItensServices.ObterEpisAVencerEmTresMesesAsync();

                if (episAVence == null || !episAVence.Any())
                {
                    return NotFound(new { message = "Nenhuma epi para a vence nos próximos três meses." });
                }

                var resultado = episAVence.Select(x => new ObterEpiAVenceViewModels
                {
                    
                    NomeCompleto = x.FichaColaborador.Colaboradores.NomeCompleto,
                    NomeEpi = x.Epis.NomeEpi,
                    DataEntrega = x.DataEntrega,
                    ValidadeEpi = x.ValidadeEpi,
                    AssinaturaEntrega = x.AssinaturaEntrega,

                })
                    .OrderBy(x => x.ValidadeEpi)
                    .ToList();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocorreu um erro interno ao processar a solicitação.",
                    error = ex.Message
                });
            }
        }



        [HttpGet]
        [Route("BuscarEpisItensEntregue")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "EPI encontrado com sucesso", Type = typeof(List<ObterEpiEntregueViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "EPI não encontrado", Type = typeof(List<ObterEpiEntregueViewModels>))]
        public async Task<IActionResult> BuscarEpisItensEntregue(int fichaId)
        {
            try
            {
              var episitens = await _unitOfWork.FichaEpiItensServices.ObterFichaEpiItensEntregue(fichaId);

                if (episitens == null || !episitens.Any())
                {
                    return NotFound(new { message = "Nenhum EPI entregue encontrado." });
                }

                var resultado = episitens
                    .Select(x => new ObterEpiEntregueViewModels
                    {
                        FichaId = x.FichaColaborador.Id,
                        NomeEpi = x.Epis.NomeEpi,
                        DataEntrega = x.DataEntrega,
                        NomeCompleto = x.FichaColaborador.Colaboradores.NomeCompleto,
                        Matricula = x.FichaColaborador.Colaboradores.Matricula,
                        AssinaturaEntrega = x.AssinaturaEntrega,
                        VencimentoEpi = x.ValidadeEpi,
                        StatusFichaEpi = StatusFichaEpiEnum.Entregue,
                    })
                    .OrderBy(x => x.VencimentoEpi) 
                    .ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocorreu um erro interno ao processar a solicitação.",
                    error = ex.Message
                });
            }
        }


        [Route("BuscarEstatisticasEpiAVence")]
        [HttpGet]
        [Authorize(Roles = "Admin,Create")]
        public async Task<IActionResult> BuscarEstatisticasEpiAVence()
        {
            var totalEpiAVence = await _unitOfWork.FichaEpiItensServices.ObterTotalEpiAVence();
            var epiAVenceMes = await _unitOfWork.FichaEpiItensServices.ObterEpiAVenceMes();

            if (totalEpiAVence == 0 )
            {
                return NotFound("Nenhum epi com vencimento no proximos tres meses  encontrado.");
            }

            var resultado = new
            {
                TotalEpiAVence = totalEpiAVence,
                EpiAVenceMes = epiAVenceMes
            };

            return Ok(resultado);
        }

        [HttpGet("BuscarEpiPorColaborador")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "EPI encontrado com sucesso", Type = typeof(List<ValidarCampos>))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "EPI não encontrado", Type = typeof(List<ValidarCampos>))]
        public async Task<IActionResult> BuscarEpiPorColaborador(int fichaId, string nome)
        {
            try
            {
                if (fichaId <= 0)
                    return BadRequest(new { message = "ID do colaborador é obrigatório." });

                var epis = await _unitOfWork.FichaEpiItensServices.ObterEpisPorColaborador(fichaId, nome);

                if (epis == null || !epis.Any())
                    return NotFound(new { message = "Nenhum EPI encontrado para esse colaborador." });

                var resultado = epis.Select(x => new ObterEpiColaboradorViewModels
                {

                    FichaId = x.FichaColaborador.Id,
                    NomeEpi = x.Epis.NomeEpi,
                    DataEntrega = x.DataEntrega,
                    NomeCompleto = x.FichaColaborador.Colaboradores.NomeCompleto,
                    Matricula = x.FichaColaborador.Colaboradores.Matricula,
                    AssinaturaEntrega = x.AssinaturaEntrega,
                    VencimentoEpi = x.ValidadeEpi,
                    StatusFichaEpi = StatusFichaEpiEnum.Entregue

                }).ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", error = ex.Message });
            }
        }
    }
}
