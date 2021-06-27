using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Services.Customers.Dto;
using Shop.Services.Customers.Queries;
using Shop.Services.Customers.Repositories;

namespace Shop.Services.Customers.Handlers.Customers
{
    public class GetCartHandler : IQueryHandler<GetCart, CartDto>
    {
        private readonly ICartsRepository _cartsRepository;

        public GetCartHandler(ICartsRepository cartsRepository)
        {
            _cartsRepository = cartsRepository;
        }

        public async Task<CartDto> HandleAsync(GetCart query)
        {
            var cart = await _cartsRepository.GetAsync(query.Id);

            return cart == null ? null : new CartDto
            {
                Id = cart.Id,
                Items = cart.Items.Select(x => new CartItemDto
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                })
            };
        }
    }
}