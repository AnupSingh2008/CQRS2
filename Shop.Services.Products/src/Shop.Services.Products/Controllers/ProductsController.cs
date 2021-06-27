using System.Threading.Tasks;
using Shop.Common.Dispatchers;
using Shop.Common.Types;
using Shop.Services.Products.Dto;
using Shop.Services.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Products.Controllers
{
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        public ProductsController(IDispatcher dispatcher)
            :base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> Get([FromQuery] BrowseProducts query)
            => Collection(await QueryAsync(query));

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAsync([FromRoute] GetProduct query)
            => Single(await QueryAsync(query));
    }
}
