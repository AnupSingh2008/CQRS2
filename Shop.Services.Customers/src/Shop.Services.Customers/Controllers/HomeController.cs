using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Customers.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Shop Customers Service");
    }
}