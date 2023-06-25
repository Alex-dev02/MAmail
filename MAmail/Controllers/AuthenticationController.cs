using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateRequestDto user)
        {
            var res = await _authenticationService.Register(user);

            if (!res.Success)
                return Conflict(res);

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var res = await _authenticationService.Login(user);

            if (!res.Success)
                return Unauthorized(res);

            return Ok(res);
        }
    }
}
