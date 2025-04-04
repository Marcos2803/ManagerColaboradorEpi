using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using Gestao_Epi.Api.Model.Colaborador;
using Gestao_Epi.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Gestao_Epi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColaboradoresController : ControllerBase

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;

        public ColaboradoresController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Colaborador cadastrado com sucesso", Type = typeof(ColaboradorViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Usuário nao autenticado", Type = typeof(ColaboradorViewModels))]
        public async Task<IActionResult> Register(ColaboradorViewModels model)
        {
            try
            {
                var matriculaExistente = await _unitOfWork.ColaboradorServices.ObterPorMatricula(model.Matricula);
                if (matriculaExistente != null)
                {
                    return BadRequest(new
                    {
                        message = "A matrícula informada já está registrada no sistema.",
                        status = 400
                    });
                }
                var colaborador = new Colaboradores
                {

                    Matricula = model.Matricula,
                    NomeCompleto = model.NomeCompleto,
                    DataCadastro = DateTime.UtcNow.Date,
                    Status = StatusColaboradorEnum.Ativo

                };


                await _unitOfWork.ColaboradorServices.Cadastro(colaborador);
                await _unitOfWork.ColaboradorServices.Salvar();

                return Ok(new
                {
                    message = $"Colaborador cadastrado com sucesso.",
                    data = new
                    {
                        colaborador.Matricula,
                        colaborador.NomeCompleto,
                        colaborador.DataCadastro,
                        colaborador.Status,


                    }
                });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        [HttpGet]
        [Route("Update/{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(200, "Informações do epi obtidas com sucesso", typeof(UpdateViewModels))]
        [SwaggerResponse(404, "Colaborador não encontrado")]
        public async Task<IActionResult> Update(int id)
        {
            var colaborador = await _unitOfWork.ColaboradorServices.ObterColaboradorPorId(id);

            if (colaborador == null)
            {
                return NotFound("Colaborador não encontrado");
            }

            var colaboradorView = new UpdateViewModels
            {

                NomeCompleto = colaborador.NomeCompleto,
                Matricula = colaborador.Matricula,


            };

            return Ok(colaboradorView);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, description: "Colaborador atualizado com sucesso", Type = typeof(UpdateViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Colaborador não encontrado", Type = typeof(UpdateViewModels))]
        public async Task<IActionResult> Update(UpdateViewModels model)
        {
            try
            {
                var colaborador = await _unitOfWork.ColaboradorServices.ObterPorMatricula(model.Matricula);
                if (colaborador == null)
                {
                    return BadRequest(new
                    {
                        message = "Erro ao encontra o colaborador.",
                        status = 400
                    });
                }

                colaborador.NomeCompleto = model.NomeCompleto;
                colaborador.Status = model.Status;


                await _unitOfWork.ColaboradorServices.Atualizar(colaborador);
                await _unitOfWork.ColaboradorServices.Salvar();

                return Ok($"Colaborador Inativado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno: " + ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Create")]
        [Route("BuscarColaboradoresAtivos")]
        [SwaggerResponse(statusCode: 200, description: "Colaborador encontrado com sucesso", Type = typeof(ColaboradorViewModels))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro internet", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 404, description: "Colaborador não encontrado", Type = typeof(ColaboradorViewModels))]
        public async Task<IActionResult> BuscarColaboradoresAtivos()
        {

            var colaboradores = await _unitOfWork.ColaboradorServices.ObterColaboradoresAtivos();
            if (colaboradores == null || !colaboradores.Any())
            {
                return NotFound();
            }
            var resultado = colaboradores.Select(u => new ColaboradorViewModels
            {

                NomeCompleto = u.NomeCompleto,
                Matricula = u.Matricula,
                DataCadastro = u.DataCadastro,
                Status = StatusColaboradorEnum.Ativo,
                Foto = await UploadHelper.ConverterImagemParaBase64Async(u.Foto)

            }).ToList();

            return Ok(resultado);

        }

        [HttpGet]
        [Route("BuscarColaboradoresPorNome")]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Colaboradores encontrados com sucesso", Type = typeof(List<ColaboradorViewModels>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum colaborador encontrado", Type = typeof(List<ColaboradorViewModels>))]
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
                    .Where(u =>
                        !string.IsNullOrWhiteSpace(u.NomeCompleto) &&
                        u.NomeCompleto.IndexOf(nome.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 
                    )
                    .Select(u => new ColaboradorViewModels
                    {
                        NomeCompleto = u.NomeCompleto,
                        Matricula = u.Matricula,
                        DataCadastro = u.DataCadastro,
                        Status = StatusColaboradorEnum.Ativo,
                    })
                    .ToList();

                if (!resultado.Any())
                {
                    return NotFound(new
                    {
                        message = $"Nenhum colaborador encontrado contendo o texto '{nome}' no nome.",
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

        [Route("BuscarEstatisticasColaboradores")]
        [HttpGet]
        [Authorize(Roles = "Admin,Create")]
        [SwaggerResponse(statusCode: 200, description: "Colaboradores encontrados com sucesso", Type = typeof(List<ValidarCampos>))]
        [SwaggerResponse(statusCode: 400, description: "Parâmetro de busca inválido", Type = typeof(ValidarCampos))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(statusCode: 404, description: "Nenhum colaborador encontrado", Type = typeof(List<ValidarCampos>))]
        public async Task<IActionResult> BuscarEstatisticasColaboradores()
        {
            var totalColaboradores = await _unitOfWork.ColaboradorServices.ObterTotalColaboradores();
            var colaboradoresNaSemana = await _unitOfWork.ColaboradorServices.ObterColaboradoresCadastradosNaSemana();

            if (totalColaboradores == 0)
            {
                return NotFound("Nenhum colaboradores encontrado.");
            }

            var resultado = new
            {
                TotalColaboradores = totalColaboradores,
                ColaboradoresNaSemana = colaboradoresNaSemana
            };

            return Ok(resultado);
        }

        [HttpPost("UpdateFoto")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(200, "Colaborador atualizado com sucesso", Type = typeof(UpdateViewModels))]
        [SwaggerResponse(400, "Campos obrigatórios", Type = typeof(ValidarCampos))]
        [SwaggerResponse(500, "Erro interno", Type = typeof(ErrosGenericos))]
        [SwaggerResponse(401, "Não autorizado", Type = typeof(ValidarCampos))]
        [SwaggerResponse(404, "Colaborador não encontrado", Type = typeof(UpdateViewModels))]
        public async Task<IActionResult> UpdateFoto([FromForm] UpDateFotoViewModels model)
        {
            try
            {
                var colaborador = await _unitOfWork.ColaboradorServices.ObterPorMatricula(model.Matricula);
                if (colaborador == null)
                {
                    return NotFound(new { message = "Colaborador não encontrado.", status = 404 });
                }

                if (model.Foto == null || model.Foto.Length == 0)
                {
                    return BadRequest(new { message = "Nenhuma foto enviada.", status = 400 });
                }

                using (var ms = new MemoryStream())
                {
                    await model.Foto.CopyToAsync(ms);
                    colaborador.Foto = ms.ToArray();
                }

                await _unitOfWork.ColaboradorServices.Atualizar(colaborador);
                await _unitOfWork.ColaboradorServices.Salvar();

                return Ok(new { message = "Foto atualizada com sucesso!", fotoUrl = colaborador.Foto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno", error = ex.Message });
            }
        }



    }
}

