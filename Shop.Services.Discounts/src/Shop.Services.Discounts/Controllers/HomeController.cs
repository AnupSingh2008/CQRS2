using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Discounts.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Shop Discounts Service");

        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}