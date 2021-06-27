using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using Shop.Api.Messages.Commands.Discount;
using Shop.Common.RabbitMq;
using Shop.Common.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shop.Api.Services;
using RestEase;

namespace Shop.Api.Controllers
{
    [AllowAnonymous]
    public class DiscountsController : BaseController
    {
        private readonly IDiscountService _discountService;
        public DiscountsController(IBusPublisher busPublisher, ITracer tracer, IDiscountService discountService) : base(busPublisher, tracer)
        {
            _discountService = discountService;

        }
        [HttpGet]
        public async Task<ActionResult<object>> Get([Query]Guid customerId)
        {
            var discount = await _discountService.GetAsync(customerId);
            return discount;
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<object>> GetDetails(Guid id)
        => Ok(Single(await _discountService.FindAsync(id)));

        [HttpPost]
        public async Task<IActionResult> Post(CreateDiscount command)
                    => await SendAsync(command.BindId(c => c.Id),
                        resourceId: command.Id, resource: "discounts");
    }
}
