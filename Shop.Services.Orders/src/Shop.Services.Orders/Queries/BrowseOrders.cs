using System;
using Shop.Common.Types;
using Shop.Services.Orders.Dto;

namespace Shop.Services.Orders.Queries
{
    public class BrowseOrders : PagedQueryBase, IQuery<PagedResult<OrderDto>>
    {
        public Guid CustomerId { get; set; }
    }
}