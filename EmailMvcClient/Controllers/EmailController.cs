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

        public EmailController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            if (!IsValidEmail(to))
            {
                ViewBag.Message = "Invalid email address.";
                return View();
            }

            var emailRequest = new
            {
                To = to,
                Subject = subject,
                Body = body
            };

            var content = new StringContent(JsonConvert.SerializeObject(emailRequest), Encoding.UTF8, "application/json");

            try
            {
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

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                return System.Text.RegularExpressions.Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
