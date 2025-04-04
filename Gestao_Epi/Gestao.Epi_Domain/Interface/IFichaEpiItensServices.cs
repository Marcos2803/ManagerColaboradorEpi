using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Interface.Base;

namespace Gestao.Epi_Domain.Interface
{
    public interface IFichaEpiItensServices : IBaseServices<FichaEpiItens>
    {
        Task<FichaEpiItens> ObterFichaEpiItensPorId(int Id);
        Task<IEnumerable<FichaEpiItens>> ObterFichaEpiItensEntregue(int fichaId);
        Task<IEnumerable<FichaEpiItens>>ObterEpisPorColaborador(int fichaId, string nome);
        Task<IEnumerable<FichaEpiItens>> ObterEpisAVencerEmTresMesesAsync();
        Task<int> ObterTotalEpiAVence();
        Task<int> ObterEpiAVenceMes();
    }
}
