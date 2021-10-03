using MongoDB.Bson;
using Some.Shared.Business.Logic;
using Some.Shared.Business.Logic.Repository;
using Some.Shared.Infra.Repository.Entities;
using System.Threading.Tasks;

namespace Some.Shared.Infra.Repository
{
    public class OrderRepository : MongoRepository, IOrderRepository
    {
        public OrderRepository(string conn, string dbName) : base(conn, dbName)
        {
        }

        public async Task SaveAsync(OrderEvent order)
        {
            var entity = OrderEntity.Map(order);

            entity.Id = ObjectId.GenerateNewId().ToString();
            await _database.GetCollection<OrderEntity>(nameof(OrderEntity)).InsertOneAsync(entity);
        }
    }
}
