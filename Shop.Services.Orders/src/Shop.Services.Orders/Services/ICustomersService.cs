using Shop.Services.Orders.Dto;
using RestEase;
using System;
using System.Threading.Tasks;

namespace Shop.Services.Orders.Services
{
    public interface ICustomersService
    {
        [AllowAnyStatusCode]
        [Get("carts/{id}")]
        Task<CartDto> GetCartAsync([Path] Guid id);
    }
}
