

using Gestao.Epi_Domain.Entities.Enumerables;

namespace Gestao.Epi_Domain.Entities
{
    public class FichaColaborador
    {
        public int Id { get; set; }
        public int ColaboradoresId { get; set; }
        public Colaboradores? Colaboradores { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusFichaEnum? StatusFicha { get; set; }


        public ICollection<FichaEpiItens>? FichaEpiItens { get; set; }

    }
}
