using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EmailServiceLibrary;
using System.Threading.Tasks;

namespace EmailApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        // Initialize EmailService with configuration
        public EmailController(IConfiguration configuration)
        {
            _emailService = new EmailService(configuration);
        }

        // HTTP POST method to send email
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            try
            {
                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
    }

    // Model class representing the email request payload
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
