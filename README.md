# Email Sending Application

This is a C# application designed to send emails to customers without interrupting or delaying user navigation. The solution includes a reusable DLL for sending emails, a console application, an API for sending emails via Postman, and an ASP.NET Core MVC front-end application.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

## Project Structure

- **EmailServiceLibrary**: A reusable DLL for sending emails.
- **EmailConsoleApp**: A console application for testing the email service.
- **EmailApi**: An ASP.NET Core Web API for sending emails.
- **EmailMvcClient**: An ASP.NET Core MVC application that provides a web interface for sending emails.

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/sarthak-p/EmailService
cd EmailService
```

### Running on Console

1) Navigate to the console: 

```bash
cd EmailConsoleApp
```

2) Build and run the application:

```bash
dotnet build
dotnet run
```

3) Enter "to" email and receive success or failure status. 

### Testing the API with Postman

1) Navigate to the API Project Directory:

```bash
cd EmailApi
```
2) Build and run the application:

```bash
dotnet build
dotnet run
```

The API will start running at http://localhost:5008.

3) Test the API with Postman:

    - Open Postman.

    - Create a new POST request:
        - URL: http://localhost:5008/api/email
        - Body: Raw JSON

    ```bash
    {
    "To": "recipient@example.com",
    "Subject": "Test Subject",
    "Body": "This is a test email."
    }
    ```

    - Send the request.

    - Verify the 200 OK response to ensure the email was sent successfully.

### Running the MVC Client Application

1) Navigate to the MVC Client Project Directory:

```bash
cd EmailMvcClient
```
2) Build and run the application:

```bash
dotnet build
dotnet run
```

3) Open your browser and navigate to:

```bash
http://localhost:5007/email/sendemail
```

- Make sure the API is running on http://localhost:5008 (From EmailApi)

4) Submit the Email Form

    


