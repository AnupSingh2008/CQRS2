using System.Threading.Tasks;
using Shop.Common.Dispatchers;
using Shop.Common.Types;
using Shop.Services.Orders.Dto;
using Shop.Services.Orders.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Orders.Controllers
{
    [Route("")]
    public class OrdersController : BaseController
    {
        public OrdersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("orders")]
        public async Task<ActionResult<PagedResult<OrderDto>>> Get([FromQuery] BrowseOrders query)
            => Collection(await QueryAsync(query));

        [HttpGet("orders/{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> Get([FromRoute] GetOrder query)
            => Single(await QueryAsync(query));
        
        [HttpGet("customers/{customerId}/orders/{orderId}")]
        public async Task<ActionResult<OrderDetailsDto>> GetForCustomer([FromRoute] GetOrder query)
            => Single(await QueryAsync(query));
    }
}
