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
    }
}
