using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Colaborador
{
    public class UpDateFotoViewModels
    {
        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public IFormFile? Foto { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }
    }
}
