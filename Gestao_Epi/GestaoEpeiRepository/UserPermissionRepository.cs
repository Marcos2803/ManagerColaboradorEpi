using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities;
using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoEpiRepository
{
    public class UserPermissionRepository : BaseRepository<UserPermission>, IUserPermissionServices
    {

        private readonly DataContext _dataContext;
        public UserPermissionRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<IEnumerable<UserPermission>> AtribuirPermissaoAsyn()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserPermission>> AtribuirPermissaoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserPermission> AtribuirPermissaoAsync(string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserPermission> ListarPermissionAsync(string UserId)
        {
            var resultado = await _dataContext.Set<UserPermission>()
                             .Where(p => p.UserId == UserId)
                             .FirstOrDefaultAsync();
            if (resultado != null)
            {
                return resultado;
            }
            return null;
        }
    }
}
