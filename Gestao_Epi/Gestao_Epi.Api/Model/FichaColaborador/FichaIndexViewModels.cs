using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpi
{
    public class FichaIndexViewModels
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int Id { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }

        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string NomeCompleto { get; set; }

        [Display(Name = "DataCadastro")]
        [Required(ErrorMessage = "Campo obrigário")]
        public DateTime DataCadastro { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusFichaEnum StatusFicha { get; set; }
    }
}
