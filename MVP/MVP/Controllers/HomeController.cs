using Microsoft.AspNetCore.Mvc;

namespace MVP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Get");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}