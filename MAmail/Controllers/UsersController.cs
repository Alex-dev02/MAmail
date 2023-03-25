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
    }
}
