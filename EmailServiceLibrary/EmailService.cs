using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmailServiceLibrary
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        // Constructor to initialize SMTP client and configuration
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient
            {
                Host = _configuration["Smtp:Host"], 
                Port = int.Parse(_configuration["Smtp:Port"]), 
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]), 
                EnableSsl = true 
            };
        }

        // Method to send email asynchronously
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var from = _configuration["Smtp:From"]; 
            var mailMessage = new MailMessage(from, to, subject, body); // Create a new mail message
            int retryCount = 0; 
            bool sent = false; 

            // Retry sending email up to 3 times if it fails
            while (retryCount < 3 && !sent)
            {
                try
                {
                    await _smtpClient.SendMailAsync(mailMessage); 
                    LogEmail(from, to, subject, body, "Success"); 
                    sent = true; 
                }
                catch (Exception ex)
                {
                    retryCount++; 
                    LogEmail(from, to, subject, body, $"Failed: {ex.Message}"); 
                    if (retryCount == 3) throw; 
                }
            }
        }

        // Method to log email details
        private void LogEmail(string from, string to, string subject, string body, string status)
        {
            string logPath = _configuration["LogPath"]; 
            using (StreamWriter writer = new StreamWriter(logPath, true)) 
            {
                writer.WriteLine($"{DateTime.Now}: From={from}, To={to}, Subject={subject}, Body={body}, Status={status}"); 
            }
        }
    }
}
