using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Interface.Base;


namespace Gestao.Epi_Domain.Interface
{
    public interface IEpiServices : IBaseServices<Epis>
    {
        Task<IEnumerable<Epis>> ObterEpisAtivos();
        Task<Epis> ObterEpiPorId(int EpiId);
        Task<int> ObterTotalEpis();
    }
}
