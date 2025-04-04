using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model
{
    public class UpDateViewModels
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int Id { get; set; }

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeEpi { get; set; }

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusEpiEnum StatusEpi { get; set; }
    }
}
