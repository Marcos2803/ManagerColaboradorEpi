using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpi
{
    public class FichaUpDateViewModels
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int Id { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusFichaEnum StatusFicha { get; set; }
    }
}
