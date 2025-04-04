using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpi
{
    public class FichaColaboradorViewModels
    {
        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }

        [Display(Name = "ColaboradoresId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int ColaboradoresId { get; set; }
    }
}
