using Microsoft.AspNetCore.Mvc;

namespace UserListApp.Server.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetUsers()
        {
            var data = new { data = "a" };
            return Ok(data);
        }
    }
}
