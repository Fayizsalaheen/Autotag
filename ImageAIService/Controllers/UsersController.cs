using ImageAIService.Exceptions;
using ImageAIService.Interface;
using ImageAIService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageAIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Service is running");
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(user);

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidEmailException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidUsernameException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DuplicateUserException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during user creation.", details = ex.Message });
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetUserById{id}")] 
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteUser{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserAsync(id);
                if (isDeleted)
                {
                    return NoContent();
                }
                return NotFound(new { message = $"User with ID {id} not found." });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during user deletion.", details = ex.Message });
            }
        }

        [HttpPut("UpdateUser{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            return StatusCode(501, new { message = "User Update is not yet implemented." });
        }
    }
}