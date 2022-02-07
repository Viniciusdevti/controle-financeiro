using Microsoft.AspNetCore.Mvc;

namespace iCommercial.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
      

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("homeController");
        }
    }
}
