using Gestao.Epi_Domain.Entities.Enumerables;

namespace Gestao.Epi_Domain.Entities
{
    public class Colaboradores
    {
        public int Id { get; set; }
        public string? Matricula { get; set; }
        public string? NomeCompleto { get; set; }
        public byte[]? Foto { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusColaboradorEnum Status { get; set; }

        public ICollection<FichaColaborador> FichaColaborador { get; set; }
    }
}
