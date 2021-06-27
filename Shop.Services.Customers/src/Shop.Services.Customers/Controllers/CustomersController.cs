using System;
using System.Threading.Tasks;
using Shop.Common.Dispatchers;
using Shop.Common.Types;
using Shop.Services.Customers.Dto;
using Shop.Services.Customers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Customers.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CustomerDto>>> Get([FromQuery] BrowseCustomers query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> Get([FromRoute] GetCustomer query)
            => Single(await QueryAsync(query));
    }
}