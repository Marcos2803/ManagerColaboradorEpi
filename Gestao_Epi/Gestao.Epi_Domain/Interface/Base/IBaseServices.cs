

namespace Gestao.Epi_Domain.Interface.Base
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task Cadastro(TEntity entity);
        Task Atualizar(TEntity entity);
        Task<TEntity> ObterPorId(int Id);
        Task<TEntity> ObterPorNome(string nome);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task Deletar(int Id);
        Task Deletar(string EpiId);
        Task Salvar();
    }
}
