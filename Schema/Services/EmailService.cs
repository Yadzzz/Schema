using System.Net.Mail;
using System.Net;

namespace Schema.Services
{
    public class EmailService
    {
        public async Task SendEmail(string email, string text)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(text))
            {
                return;
            }

            await Console.Out.WriteLineAsync(email);
            await Console.Out.WriteLineAsync(text);

            try
            {
                //SMTP settings
                string smtpServer = "send.one.com";
                int smtpPort = 587; // The port number may vary depending on your email provider
                string smtpUsername = "no-reply@allticonsulting.se";
                string smtpPassword = "yadshad12";

                // Email information
                string fromAddress = "no-reply@allticonsulting.se";
                string toAddress = email;
                string subject = "Västkustbevakning";
                string body = text;

                // Create a new MailMessage
                using (MailMessage message = new MailMessage(fromAddress, toAddress, subject, body))
                {
                    // Create an instance of SmtpClient
                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true;

                        // Send the email
                        smtpClient.Send(message);
                    }
                }

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
