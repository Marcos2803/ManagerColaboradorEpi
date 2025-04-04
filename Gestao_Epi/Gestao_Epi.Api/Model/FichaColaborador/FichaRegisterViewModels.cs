using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpi
{
    public class FichaRegisterViewModels
    {

        [Display(Name = "ColaboradoresId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int ColaboradoresId { get; set; }
    }
}
