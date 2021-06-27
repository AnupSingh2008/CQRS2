using Shop.Common.Types;
using Shop.Api.Models.Products;
using Shop.Api.Queries;
using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<Product> GetAsync([Path] Guid id);

        [AllowAnyStatusCode]
        [Get("products")]
        Task<PagedResult<Product>> BrowseAsync([Query] BrowseProducts query);
    }
}
