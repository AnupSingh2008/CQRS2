using System;
using System.Collections.Generic;
using static Shop.Services.Orders.Domain.Order;

namespace Shop.Services.Orders.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
    }
}
