using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Interface.Base;

namespace Gestao.Epi_Domain.Interface
{
    public interface IColaboradorServices : IBaseServices<Colaboradores>
    {
        Task<IEnumerable<Colaboradores>> ObterColaboradoresAtivos ();
        Task<Colaboradores> ObterColaboradorPorId(int Id);
        Task<Colaboradores> ObterPorMatricula(string matricula);
        Task<int> ObterTotalColaboradores();
        Task<int> ObterColaboradoresCadastradosNaSemana();
    }
}
