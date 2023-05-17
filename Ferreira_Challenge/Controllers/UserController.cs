using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs.Input;
using Services;
using Utils.Enums;

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

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);

            if (user.Result == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = await _userService.CreateUser(createUserDTO);

            return Ok(new { userId = userId });
        }

        // PUT api/user/{id}
        [HttpPut]
        public async Task<ActionResult<User>> Update(int id, UpdateUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id) { return BadRequest("Invalid user ID"); }

            _userService.UpdateUser(user);

            return Ok(user);
        }

        // DELETE api/user/{id}
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null || user.Status == Status.Inactive)
            {
                return NotFound();
            }

            var deletedUser = await _userService.DeleteUser(id);

            return Ok(deletedUser);
        }
    }
}