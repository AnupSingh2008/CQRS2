using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Common.Handlers;
using Shop.Common.Types;
using Shop.Services.Orders.Dto;
using Shop.Services.Orders.Queries;
using Shop.Services.Orders.Repositories;

namespace Shop.Services.Orders.Handlers.Orders
{
    public class BrowseOrdersHandler : IQueryHandler<BrowseOrders, PagedResult<OrderDto>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public BrowseOrdersHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<PagedResult<OrderDto>> HandleAsync(BrowseOrders query)
        {
            var pagedResult = await _ordersRepository.BrowseAsync(query);
            var orders = pagedResult.Items.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                ItemsCount = o.Items.Count(),
                TotalAmount = o.TotalAmount,
                Currency = o.Currency,
                Status = o.Status.ToString().ToLowerInvariant()
            });

            return PagedResult<OrderDto>.From(pagedResult, orders);
        }
    }
}