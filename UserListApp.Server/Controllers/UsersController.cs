using Microsoft.AspNetCore.Mvc;

namespace UserListApp.Server.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("Users")]
        public ActionResult GetUsers()
        {
            var data = new { data = "a" };
            return Ok(data);
        }
    }
}
