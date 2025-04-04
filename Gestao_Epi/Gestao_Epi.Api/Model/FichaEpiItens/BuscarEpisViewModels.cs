using System.ComponentModel.DataAnnotations;

namespace Gestao_Epi.Api.Model.FichaEpiItens
{
    public class BuscarEpisViewModels
    {

        [Display(Name = "NomeEpi")]
        [Required(ErrorMessage = "Campo obrigário")]
        public string? NomeEpi { get; set; }

        [Display(Name = "EpisId")]
        [Required(ErrorMessage = "Campo obrigário")]
        public int EpisId { get; set; }

 
    }
}
