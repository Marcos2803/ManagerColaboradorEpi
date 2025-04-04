using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpiItens
{
    public class ObterEpiAVenceViewModels
    {
       

        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeEpi { get; set; }

        [Display(Name = "DataEntrega")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime? DataEntrega { get; set; }

        [Display(Name = "ValidadeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime? ValidadeEpi { get; set; }

        [Display(Name = "AssinaturaEntrega")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? AssinaturaEntrega { get; set; }

    }
}
