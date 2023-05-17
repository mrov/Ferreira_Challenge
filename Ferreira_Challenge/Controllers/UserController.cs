using Models;
using Services;
using System.Web.Http;

namespace Ferreira_Challenge.Controllers
{

    public class UserController : ApiController
    {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET api/user/{id}
    [HttpGet]
    public IHttpActionResult Get(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // GET api/user
    [HttpGet]
    public IHttpActionResult GetAll()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    // POST api/user
    [HttpPost]
    public IHttpActionResult Create(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _userService.CreateUser(user);

        return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
    }

    // PUT api/user/{id}
    [HttpPut]
    public IHttpActionResult Update(int id, User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != user.Id)
        {
            return BadRequest("Invalid user ID");
        }

        _userService.UpdateUser(user);

        return Ok(user);
    }

    // DELETE api/user/{id}
    [HttpDelete]
    public IHttpActionResult Delete(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        _userService.DeleteUser(id);

        return Ok(user);
    }
}

}