

namespace Gestao.Epi_Domain.Entities
{
    public class Estoque
    {
        public int Id { get; set; }
        public int EstoqueMinimo { get; set; }
        public int Quantidade { get; set; }
        public int EpisId { get; set; }
        public Epis Epis { get; set; }
    }
}
