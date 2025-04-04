

using Gestao.Epi_Domain.Entities.Enumerables;

namespace Gestao.Epi_Domain.Entities
{
    public class Epis
    {
        public int Id { get; set; }
        public string? NomeEpi { get; set; }
        public string? CertificadoAprovacao { get; set; }
        public string? Fabricante { get; set; } 
        public StatusEpiEnum? StatusEpi { get; set; }

        public ICollection<FichaEpiItens> FichaEpiItens { get; set; }
        public ICollection<Estoque> Estoque { get; set; }



    }
}
