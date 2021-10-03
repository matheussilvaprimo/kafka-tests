using MongoDB.Driver;

namespace Some.Shared.Infra.Repository
{
    public abstract class MongoRepository
    {
        protected readonly IMongoDatabase _database;
        public MongoRepository(string conn, string dbName) => _database = new MongoClient(conn)?.GetDatabase(dbName);
    }
}
