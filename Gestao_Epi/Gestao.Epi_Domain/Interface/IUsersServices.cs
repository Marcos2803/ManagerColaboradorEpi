using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface.Base;

namespace Gestao.Epi_Domain.Interface
{
    public interface IUsersServices : IBaseServices<User>
    {
        Task<IEnumerable<User>> ObterUsuariosAtivos();
        Task<User> ObterPorMatricula(string matricula);
        Task<int> ObterTotalUsuarios();
    }
}
