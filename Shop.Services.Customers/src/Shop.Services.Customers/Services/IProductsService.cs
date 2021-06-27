using System;
using System.Threading.Tasks;
using Shop.Services.Customers.Dto;
using RestEase;

namespace Shop.Services.Customers.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<ProductDto> GetAsync([Path] Guid id);
    }
}