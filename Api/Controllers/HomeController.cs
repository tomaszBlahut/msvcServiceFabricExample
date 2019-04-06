using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : BasicController
    {
        [HttpGet]
        public IActionResult GetHome()
        {
            return Ok("Msvc API");
        }
    }
}