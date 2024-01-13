
using ClearXchange.Server.Model;
using MongoDB.Driver;
namespace ClearXchange.Server.Data
{
    public class MongoDbContext
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Member> Members
        {
            get { return _database.GetCollection<Member>("members"); }
        }
    }
}
