using Microsoft.AspNetCore.Mvc;

namespace Shop.Services.Identity.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Shop Identity Service");
    }
}