using System.Linq.Expressions;
using EcotrackBusiness.Models;

namespace EcotrackBusiness.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        // Adiciona uma nova instância da entidade no repositorio
        Task Adicionar(TEntity entity);

        // Obtém uma entidade pelo Id
        Task<TEntity> ObterPorId(Guid id);

        // Busca uma entidade com uma expressao lambda
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);

        // Salva as alteracoes feitas no repositorio
        Task<int> SaveChanges();
    } 
}