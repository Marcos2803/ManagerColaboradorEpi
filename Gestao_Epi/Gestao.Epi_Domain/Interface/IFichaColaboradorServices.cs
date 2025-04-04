using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Interface.Base;

namespace Gestao.Epi_Domain.Interface
{
    public interface IFichaColaboradorServices : IBaseServices<FichaColaborador>
    {
        Task<FichaColaborador> ObterFichaEpiPorId(int Id);
        Task<FichaColaborador> ObterFichaPorColaboradorId(int Id);
        Task<IEnumerable<FichaColaborador>> ObterFichaEpiAtivo();

    }
}
