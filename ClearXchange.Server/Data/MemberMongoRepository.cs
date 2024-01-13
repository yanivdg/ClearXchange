using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Model;
using MongoDB.Driver.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClearXchange.Server.Data
{
    public class MemberMongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<Member> _collection;


        public MemberMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Member>("Members");
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Member> GetById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<Member>.Filter.Eq("_id", objectId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Member>> Search(Expression<Func<Member, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task Create(Member entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(Member entity)
        {
            var filter = Builders<Member>.Filter.Eq("_id", entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<Member>.Filter.Eq("_id", objectId);
            await _collection.DeleteOneAsync(filter);
        }

        Task<T> IRepository<T>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<T>> IRepository<T>.GetAll()
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
    }

}
