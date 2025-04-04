using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model
{
    public class EpiIndexViewModels
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int Id { get; set; }

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeEpi { get; set; }

        [Display(Name = "Fabricante")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Fabricante { get; set; }

        [Display(Name = "CertificadoAprovação")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? CertificadoAprovacao { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusEpiEnum StatusEpi { get; set; }
    }
}
