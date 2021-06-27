using System;
using Shop.Common.Types;
using Shop.Services.Customers.Dto;

namespace Shop.Services.Customers.Queries
{
    public class GetCart : IQuery<CartDto>
    {
        public Guid Id { get; set; }
    }
}