using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Orders.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Shop Orders Service");
    }
}
