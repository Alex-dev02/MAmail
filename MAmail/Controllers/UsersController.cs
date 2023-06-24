using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private UserService _userService;
        
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/get/{userId}")]
        public async Task<ActionResult<UserGetByIdResponseDto>> GetUserById(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPatch("/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto user)
        {
            var success = await _userService.UpdateUser(user);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete("/delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var success = await _userService.DeleteUser(userId);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
