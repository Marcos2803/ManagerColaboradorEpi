using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpiItens
{
    public class ObterEpiEntregueViewModels
    {

        [Display(Name = "FichaId ")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int FichaId { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }

        [Display(Name = "DataEntrega ")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime? DataEntrega { get; set; }

        [Display(Name = "AssinaturaEntrega")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? AssinaturaEntrega { get; set; }

        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeEpi { get; set; }

        [Display(Name = "VencimentoEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime? VencimentoEpi { get; set; }

        [Display(Name = "StatusFicha")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusFichaEpiEnum StatusFichaEpi { get; set; }
    }
}
