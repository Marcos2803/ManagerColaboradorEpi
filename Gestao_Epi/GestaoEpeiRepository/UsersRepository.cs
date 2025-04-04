using gestao.EpiData.Context;
using Gestao.Epi_Domain.Entities.Account;
using Gestao.Epi_Domain.Entities.Enumerables;
using Gestao.Epi_Domain.Interface;
using GestaoEpiRepository.Base;
using Microsoft.EntityFrameworkCore;

namespace GestaoEpiRepository
{
    public class UsersRepository : BaseRepository<User>, IUsersServices
    {
        private readonly DataContext _dataContext;
        public UsersRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<User>> ObterUsuariosAtivos()
        {
            var resultado = await _dataContext.Set<User>()
                 .Where(x => x.StatusUser == StatusUserEnum.Ativo)
                 .ToListAsync();

            if (resultado != null)
            {
                return resultado;
            }

            return null;
        }

        public async Task<User> ObterPorMatricula(string matricula)
        {
            var resultado = await _dataContext.Set<User>()
                .Where(x => x.Matricula == matricula)
                .FirstOrDefaultAsync();
            if (resultado != null)
            {
                return resultado;
            }
            return null;
        }

        public async Task<int> ObterTotalUsuarios()
        {
            return await _dataContext.Users
       .Where(x => x.StatusUser == StatusUserEnum.Ativo)
       .CountAsync();
        }
    }
}
