using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Auth
{
    public class UsersViewModels
    {
        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }
    }
}
