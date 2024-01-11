using ClearXchange.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClearXchange.Server.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
