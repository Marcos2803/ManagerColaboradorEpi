using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Auth
{
    public class UpdateUserViewModels
    {

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }


        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusUserEnum Status { get; set; }
    }
}
