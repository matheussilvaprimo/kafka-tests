using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Some.Shared.Business.Logic;
using System;
using System.Collections.Generic;

namespace Some.Shared.Infra.Repository.Entities
{
    public class OrderEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public static OrderEntity Map(OrderEvent order)
        {
            return new OrderEntity
            {
                BuyerId = order.BuyerId,
                Description = order.Description,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems,
                Status = order.Status,
                Address = order.Address
            };
        }
    }
}
