using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Some.Shared.Business.Logic
{
    public class OrderEvent
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public string Description { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }

    public class OrderItem
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
    }

    public enum OrderStatus
    {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
}
