using ClearXchange.Server.Model;
using System.Linq.Expressions;

namespace ClearXchange.Server.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> Search(Expression<Func<User, bool>> predicate);
        Task<IEnumerable<User>> GetAllUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
