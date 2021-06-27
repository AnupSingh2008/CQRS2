using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Common.Dispatchers;
using Shop.Common.Mvc;
using Shop.Services.Discounts.Dto;
using Shop.Services.Discounts.Messages.Commands;
using Shop.Services.Discounts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Discounts.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public DiscountsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        // Idempotent
        // No side effects
        // Doesn't mutate a state
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> Get([FromQuery] FindDiscounts query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiscountDetailsDto>> GetDetails([FromRoute] FindDiscounts query)
        => Ok(await _dispatcher.QueryAsync(query));

        [HttpPost]
        public async Task<ActionResult> Post(CreateDiscount command)
        {
            await _dispatcher.SendAsync(command.BindId(c => c.Id));

            return Accepted();
        }
    }
}