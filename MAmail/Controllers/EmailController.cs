using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/email")]
    public class EmailController
    {
        private EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("/send")]
        public async Task<IActionResult> Send([FromBody] EmailSendRequestDto emails)
        {

        }

    }
}
