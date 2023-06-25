using MAmail.Dtos;
using MAmail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MAmail.Controllers
{
    [ApiController]
    [Authorize]
    [Route("emails")]
    public class EmailController : ControllerBase
    {
        private EmailService _emailService;
        private RecipientService _recipientService;

        public EmailController(EmailService emailService, RecipientService recipientService)
        {
            _emailService = emailService;
            _recipientService = recipientService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailSendRequestDto emails)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            try
            {
                await _emailService.Send(userId, emails);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }

        [HttpGet("all-received")]
        public async Task<IActionResult> GetReceivedEmails(int pageNumber)
        {
            if (pageNumber < 1)
                return BadRequest("Invalid page number!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            return Ok(await _emailService.GetReceivedEmails(userId, pageNumber));
        }

        [HttpGet("all-sent")]
        public async Task<IActionResult> GetSentEmails(int pageNumber)
        {
            if (pageNumber < 1)
                return BadRequest("Invalid page number!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            return Ok(await _emailService.GetSentEmails(userId, pageNumber));
        }

        [HttpGet("all-unread")]
        public async Task<IActionResult> GetUnreadEmails(int pageNumber)
        {
            if (pageNumber < 1)
                return BadRequest("Invalid page number!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            return Ok(await _recipientService.GetUnreadEmails(userId, pageNumber));
        }

        [HttpGet("all-archived")]
        public async Task<IActionResult> GetArchivedEmails(int pageNumber)
        {
            if (pageNumber < 1)
                return BadRequest("Invalid page number!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            return Ok(await _recipientService.GetArchivedEmails(userId, pageNumber));
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetEmailById(int id)
        {
            if (id < 1)
                return BadRequest("Invalid email id!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            return Ok(await _emailService.GetEmailById(userId, id));
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkEmailsAsRead(int id)
        {
            if (id < 1)
                return BadRequest("Invalid email id!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);
            
            var res = await _recipientService.MarkAsRead(userId, id);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPut("archive/{id}")]
        public async Task<IActionResult> ArchiveEmail(int id)
        {
            if (id < 1)
                return BadRequest("Invalid email id!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            var res = await _recipientService.ArchiveEmail(userId, id);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            if (id < 1)
                return BadRequest("Invalid email id!");

            int userId = int.Parse(this.User.Claims.First(i => i.Type == "userId").Value);

            var res = await _recipientService.DeleteEmail(userId, id);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
