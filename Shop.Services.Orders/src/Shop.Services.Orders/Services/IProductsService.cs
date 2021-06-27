using System;
using System.Threading.Tasks;
using Shop.Services.Orders.Dto;
using RestEase;

namespace Shop.Services.Orders.Services
{
    public interface IProductsService
    {
        [AllowAnyStatusCode]
        [Get("products/{id}")]
        Task<OrderItemDto> GetAsync([Path] Guid id);
    }
}