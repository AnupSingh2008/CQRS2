using System;

namespace Shop.Services.Customers.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set ;}
        public Guid CustomerId { get; set; }
    }
}