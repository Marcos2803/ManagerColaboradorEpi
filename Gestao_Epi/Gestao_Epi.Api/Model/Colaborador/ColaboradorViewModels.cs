using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.Colaborador
{
    public class ColaboradorViewModels
    {
        

        [Display(Name = "NomeCompleto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeCompleto { get; set; }

        [Display(Name = "Matricula")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Matricula { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Campo obrigário")]
        public StatusColaboradorEnum Status { get; set; }

        [Display(Name = "Foto")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? Foto { get; set; }

    }
}
