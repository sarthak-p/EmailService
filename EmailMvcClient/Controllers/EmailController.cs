using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace EmailMvcClient.Controllers
{
    public class EmailController : Controller
    {
        private readonly HttpClient _httpClient;

        // Constructor to initialize HttpClient
        public EmailController()
        {
            _httpClient = new HttpClient();
        }

        // GET method to render the SendEmail view
        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        // POST method to handle form submission and send email
        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            // Validate the recipient email address
            if (!IsValidEmail(to))
            {
                ViewBag.Message = "Invalid email address.";
                return View();
            }

            // Create an email request object
            var emailRequest = new
            {
                To = to,
                Subject = subject,
                Body = body
            };

            // Serialize the email request object to JSON
            var content = new StringContent(JsonConvert.SerializeObject(emailRequest), Encoding.UTF8, "application/json");

            try
            {
                // Send POST request to the API
                var response = await _httpClient.PostAsync("http://localhost:5008/api/email", content);
                var result = await response.Content.ReadAsStringAsync();

    
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Email sent successfully.";
                }
                else
                {
                    ViewBag.Message = $"Failed to send email: {result}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Failed to send email: {ex.Message}";
            }

            return View();
        }

        // Method to validate email addresses using a regular expression
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
