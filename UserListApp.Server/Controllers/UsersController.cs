using Microsoft.AspNetCore.Mvc;
using UserListApp.Application.DTO;
using UserListApp.Application.Services;

namespace UserListApp.Server.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;

    public UsersController(IUserService _userService)
    {
        userService = _userService;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet]
    [Route("search")]
    public async Task<ActionResult> SearchUsersByNames([FromQuery] string[] names)
    {
        var users = await userService.GetByNamesAsync(names);

        return Ok(users);
    }

    [HttpGet("page")]
    public async Task<ActionResult> GetPage([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {

        if (pageNumber < 0)
        {
            pageNumber = 0;
        }

        if (pageSize <= 0)
        {
            pageSize = 10;
        }

        var users = await userService.GetPageAsync(pageNumber, pageSize);

        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(UserDTO user)
    {
        if (user == null)
        {
            return BadRequest("User data is required.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await userService.AddAsync(user);

        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDTO user)
    {
        if (user == null || id != user.Id)
        {
            return BadRequest("User ID mismatch or invalid user data.");
        }

        if (!TryValidateModel(user))
        {
            return BadRequest(ModelState);
        }

        await userService.UpdateAsync(user);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUsers(int id)
    {
        await userService.DeleteAsync(id);

        return Ok();
    }
}
