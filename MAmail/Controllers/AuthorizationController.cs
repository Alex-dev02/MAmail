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
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequestDto user)
        {
            var res = await _authorizationService.Register(user);

            return Ok(res);
        }
    }
}
