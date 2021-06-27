using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Common.Types;
using Shop.Api.Models.Customers;
using Shop.Api.Queries;
using RestEase;

namespace Shop.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("customers/{id}")]
        Task<Customer> GetAsync([Path] Guid id);  

        [AllowAnyStatusCode]
        [Get("customers")]
        Task<PagedResult<Customer>> BrowseAsync([Query] BrowseCustomers query);

        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<Cart> GetCartAsync([Path] Guid id);  
    }
}