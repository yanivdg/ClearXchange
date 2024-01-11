using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using System.Linq.Expressions;

namespace ClearXchange.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> Search(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
