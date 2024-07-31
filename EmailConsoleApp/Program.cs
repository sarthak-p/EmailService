using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using EmailServiceLibrary;

class Program
{
    static async Task Main(string[] args)
    {
        // Build configuration to read settings from appsettings.json
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Initialize email service with the configuration
        var emailService = new EmailService(configuration);

        Console.WriteLine("Enter recipient email:");
        string recipient = Console.ReadLine();

        try
        {
            // Send an email asynchronously
            await emailService.SendEmailAsync(recipient, "Test subject", "Hi, this is a test message.");
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}
