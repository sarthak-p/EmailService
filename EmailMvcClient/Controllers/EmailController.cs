using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            var emailRequest = new
            {
                To = to,
                Subject = subject,
                Body = body
            };

            var content = new StringContent(JsonConvert.SerializeObject(emailRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5008/api/email", content);
            var result = await response.Content.ReadAsStringAsync();

            ViewBag.Message = result;
            return View();
        }
    }
}
