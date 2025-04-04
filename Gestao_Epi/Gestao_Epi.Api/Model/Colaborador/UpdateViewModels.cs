using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Colaborador
{
    public class UpdateViewModels
    {
        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }


        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusColaboradorEnum Status { get; set; }
    }
}
