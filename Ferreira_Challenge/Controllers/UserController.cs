using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOs.Input;
using Models.DTOs.Output;
using Services;
using Utils;
using Utils.Enums;

namespace Ferreira_Challenge.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
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
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET api/user
        [HttpGet]
        public async Task<IActionResult> GetFilteredUsers([FromQuery] UserFilterDTO filter)
        {
            try
            {
                UserPagination userPagination = await _userService.GetFilteredUsers(filter);
                return Ok(userPagination);
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userService.IsUserExists(createUserDTO.Login))
            {
                return Conflict("User already exists");
            }

            var userId = await _userService.CreateUser(createUserDTO);

            return Ok(new { userId = userId });
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(int id, UpdateUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool canUpdate = await _userService.CanUpdate(id, user.Login);

            if (!canUpdate)
            {
                return Conflict("New User Login already exists");
            }

            if (id != user.Id) { return BadRequest("Invalid user ID"); }

            await _userService.UpdateUser(user);

            return Ok(user);
        }


        [HttpPut("{id}/Status")]
        public async Task<IActionResult> UpdateUserStatus(int id, [FromBody] UserStatusUpdateDto statusDto)
        {
            try
            {
                Status status = await _userService.UpdateUserStatus(id, statusDto.Status);
                return Ok(status);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Handle the exception and return an appropriate error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
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

        // DELETE Api/User/DeleteAll
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllUsers()
        {
            await _userService.DeleteAllUsers();

            // Handle the response as needed
            return Ok();
        }
    }
}