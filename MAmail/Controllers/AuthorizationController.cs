using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Route("/authorization")]
    public class AuthorizationController : ControllerBase
    {
        private AuthorizationService _authorizationService;

        public AuthorizationController(AuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserCreateRequestDto user)
        {
            var res = await _authorizationService.Register(user);

            if (!res.Success)
                return Conflict(res);

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var res = await _authorizationService.Login(user);

            if (!res.Success)
                return Unauthorized(res);

            return Ok(res);
        }
    }
}
