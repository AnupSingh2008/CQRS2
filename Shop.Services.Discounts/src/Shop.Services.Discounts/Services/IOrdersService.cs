using System;
using System.Threading.Tasks;
using Shop.Services.Discounts.Dto;
using RestEase;

namespace Shop.Services.Discounts.Services
{
    public interface IOrdersService
    {
        [AllowAnyStatusCode]
        [Get("orders/{id}")]
        Task<OrderDetailsDto> GetAsync([Path] Guid id);
    }
}