using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClearXchange.Server.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }

}
