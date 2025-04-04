using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpiItens
{
    public class FichaEpiItensViewModels
    {
        public int Id { get; set; }

        [Display(Name = "EpisId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int EpiId { get; set; }

        [Display(Name = "FichaId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int FichaId { get; set; }

        [Display(Name = "ValidadeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime? ValidadeEpi { get; set; }
    }
}
