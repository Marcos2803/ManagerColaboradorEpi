
using gestao.EpiData.Context;
using Gestao.Epi_Domain.Interface;

namespace GestaoEpiRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private IUsersServices _usersServices;
        private IEpiServices _epiServices;
        private IFichaColaboradorServices _fichaColaboradorServices;
        private IColaboradorServices _colaboradorServices;
        private IFichaEpiItensServices _fichaEpiItensServices;
        private IUserPermissionServices _userPermissionServices;


        public IEpiServices EpiServices => _epiServices ??= new EpiRepository(_dataContext);

        public IColaboradorServices ColaboradorServices => _colaboradorServices ??= new ColaboradorRepository(_dataContext);

        public IFichaEpiItensServices FichaEpiItensServices => _fichaEpiItensServices ??= new FichaEpiItensRepository(_dataContext);

        public IFichaColaboradorServices FichaColaboradorServices => _fichaColaboradorServices ??= new FichaColaboradorRepository(_dataContext);

        public IUsersServices UserServices => _usersServices ??= new UsersRepository(_dataContext);

        public IUserPermissionServices UserPermissionServices => _userPermissionServices ??= new UserPermissionRepository(_dataContext);
    }
}
