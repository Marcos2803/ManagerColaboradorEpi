namespace Gestao.Epi_Domain.Interface
{
    public interface IUnitOfWork
    {
        IUsersServices UserServices { get; }
        IEpiServices EpiServices { get; }
        IFichaEpiItensServices FichaEpiItensServices { get; }
        IFichaColaboradorServices FichaColaboradorServices { get; }
        IColaboradorServices ColaboradorServices { get; }
        IUserPermissionServices UserPermissionServices { get; }


    }
}