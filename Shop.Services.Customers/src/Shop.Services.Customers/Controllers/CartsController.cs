using System;
using System.Threading.Tasks;
using Shop.Common.Dispatchers;
using Shop.Services.Customers.Dto;
using Shop.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Customers.Controllers
{
    public class CartsController : BaseController
    {
        public CartsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> Get([FromRoute] GetCart query)
            => Single(await QueryAsync(query));
    }
}