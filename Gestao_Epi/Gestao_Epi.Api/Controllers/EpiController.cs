using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EpiController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public EpiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epi cadastrado com sucesso", Type = typeof(EpiViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Usuário nao autenticado", Type = typeof(EpiViewModels))]
        public async Task<IActionResult> Register(EpiViewModels model)
        {
            try
            {
                var epis = new Epis
                {
                    Id = model.Id,
                    NomeEpi = model.NomeEpi,
                    Fabricante = model.Fabricante,
                    CertificadoAprovacao = model.CertificadoAprovacao,
                    StatusEpi = StatusEpiEnum.Ativo

                };


                await _unitOfWork.EpiServices.Cadastro(epis);
                await _unitOfWork.EpiServices.Salvar();

                return Ok(new
                {
                    message = $"Epi cadastrado com sucesso.",
                    data = new
                    {
                        epis.Id,
                        epis.NomeEpi,
                        epis.Fabricante,
                        epis.CertificadoAprovacao,
                        
                    }
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
 

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, description: "Epi atualizado com sucesso", Type = typeof(UpDateViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Epi não encontrado", Type = typeof(UpDateViewModels))]
        public async Task<IActionResult> Update(UpDateViewModels model)
        {
            try
            {
                var epis = await _unitOfWork.EpiServices.ObterEpiPorId(model.Id);
                if (epis == null)
                {
                    return NotFound("Epi não encontrado");
                }
                epis.NomeEpi = model.NomeEpi;
                epis.StatusEpi = model.StatusEpi;

                await _unitOfWork.EpiServices.Atualizar(epis);
                await _unitOfWork.EpiServices.Salvar();

                return Ok($"Epi inativado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("BuscarEpisAtivos")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epis encontrado com sucesso", Type = typeof(EpiIndexViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Epi não encontrado", Type = typeof(EpiIndexViewModels))]
        public async Task<IActionResult> BuscarEpisAtivos()
        {

            var epis = await _unitOfWork.EpiServices.ObterEpisAtivos();
            if (epis == null || !epis.Any())
            {
                return NotFound();
            }
            var resultado = epis.Select(u => new EpiIndexViewModels
            {
                Id = u.Id,
                NomeEpi = u.NomeEpi,
                Fabricante = u.Fabricante,
                CertificadoAprovacao = u.CertificadoAprovacao,
                StatusEpi = StatusEpiEnum.Ativo,
                

            }).ToList();

            return Ok(resultado);


        }

        [Route("BuscarEstatisticasEpis")]
        [HttpGet]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epis encontrados com sucesso", Type = typeof(List<EpiViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        public async Task<IActionResult> BuscarEstatisticasEpiss()
        {
            var totalEpis = await _unitOfWork.EpiServices.ObterTotalEpis();
           

            if (totalEpis == 0)
            {
                return NotFound("Nenhum Epi encontrado.");
            }

            var resultado = new
            {
                TotalEpisAtivos = totalEpis,
            };

            return Ok(resultado);
        }

        [HttpGet]
        [Route("BuscarEpisPorNome")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Epis encontrados com sucesso", Type = typeof(List<EpiViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum epi encontrado", Type = typeof(List<EpiViewModels>))]
        public async Task<IActionResult> BuscarEpisPorNome(string nome)
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
                var epis = await _unitOfWork.EpiServices.ObterEpisAtivos();
                if (epis == null || !epis.Any())
                {
                    return NotFound(new
                    {
                        message = "Nenhum epi ativo encontrado.",
                        status = 404
                    });
                }

                var resultado = epis
                    .Where(u =>
                        !string.IsNullOrWhiteSpace(u.NomeEpi) &&
                        u.NomeEpi.IndexOf(nome.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    .Select(u => new EpiViewModels
                    {
                        Id = u.Id,
                        NomeEpi = u.NomeEpi,
                        Fabricante = u.Fabricante,
                        CertificadoAprovacao = u.CertificadoAprovacao,
                        StatusEpi = StatusEpiEnum.Ativo,
                    })
                    .ToList();

                if (!resultado.Any())
                {
                    return NotFound(new
                    {
                        message = $"Nenhum epi encontrado contendo o texto '{nome}' no nome.",
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
    }
}
