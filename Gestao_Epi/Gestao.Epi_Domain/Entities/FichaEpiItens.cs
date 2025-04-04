

using Gestao.Epi_Domain.Entities.Enumerables;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Epi_Domain.Entities
{
    public class FichaEpiItens
    {
        public int Id { get; set; }
        public int EpisId { get; set; }
        public Epis? Epis { get; set; }
        public int? FichaId { get; set; }
        [ForeignKey("FichaId")]
        public FichaColaborador? FichaColaborador { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string? AssinaturaEntrega { get; set; }
        public DateTime? ValidadeEpi { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public string? AssinaturaDevolucao { get; set; }
        public StatusFichaEpiEnum? StatusFichaEpi { get; set; }

    }
}
