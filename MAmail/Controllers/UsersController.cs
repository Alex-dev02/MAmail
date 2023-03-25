using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private UserService userService { get; set; }
        
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/create")]
        public ActionResult CreateUser([FromBody] UserCreateRequestDto user)
        {
            userService.CreateUser(user);

            return Ok();
        }
        [HttpGet("/get/{userId}")]
        public ActionResult<UserGetByIdResponseDto> GetUserById(int userId)
        {
            var user = userService.GetUserById(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [HttpPatch("/update")]
        public ActionResult UpdateUser([FromBody] UserUpdateDto user)
        {
            var success = userService.UpdateUser(user);

            if (!success)
                return NotFound();

            return Ok();
        }
        [HttpDelete("/delete/{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            var success = userService.DeleteUser(userId);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
