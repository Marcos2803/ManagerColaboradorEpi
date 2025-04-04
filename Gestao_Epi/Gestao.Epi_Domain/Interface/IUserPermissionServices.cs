

using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface.Base;

namespace Gestao.Epi_Domain.Interface
{
    public interface IUserPermissionServices : IBaseServices<UserPermission>
    {
        Task<IEnumerable<UserPermission>> AtribuirPermissaoAsync();
        Task<UserPermission> ListarPermissionAsync(string UserId);
    }
}
