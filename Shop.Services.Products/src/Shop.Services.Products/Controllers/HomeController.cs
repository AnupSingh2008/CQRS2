using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Products.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Shop Products Service");
    }
}