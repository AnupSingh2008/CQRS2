using System;
using Shop.Common.Types;
using Shop.Services.Orders.Dto;

namespace Shop.Services.Orders.Queries
{
    public class GetOrder : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}