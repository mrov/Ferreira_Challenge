using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs.Input;
using Services;

namespace Ferreira_Challenge.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    //// GET api/user/{id}
    //[HttpGet]
    //public async Task<ActionResult<User>> GetUserById(int id)
    //{
    //    var user = _userService.GetUserById(id);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    return Ok(user);
    //}

    // GET api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    // POST api/user
    [HttpPost]
    public async Task<ActionResult<User>> Create(CreateUserDTO user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _userService.CreateUser(user);

        return Ok(user);
    }

    //// PUT api/user/{id}
    //[HttpPut]
    //public async Task<ActionResult<User>> Update(int id, User user)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    if (id != user.Id)
    //    {
    //        return BadRequest("Invalid user ID");
    //    }

    //    _userService.UpdateUser(user);

    //    return Ok(user);
    //}

    //// DELETE api/user/{id}
    //[HttpDelete]
    //public async Task<ActionResult> Delete(int id)
    //{
    //    var user = _userService.GetUserById(id);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    _userService.DeleteUser(id);

    //    return Ok(user);
    //}
}

}