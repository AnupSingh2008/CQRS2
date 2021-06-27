using System;
using Shop.Common.Types;
using Shop.Services.Customers.Dto;

namespace Shop.Services.Customers.Queries
{
    public class GetCustomer : IQuery<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}