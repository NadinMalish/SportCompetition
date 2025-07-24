using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;


namespace AppAutority.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Администратор сайта", AppValues.MailSender));
                emailMessage.To.Add(new MailboxAddress(email, email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
                emailMessage.Body = new BodyBuilder()
                {
                    HtmlBody = htmlMessage
                }.ToMessageBody(); 


                using (SmtpClient client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync(AppValues.MailSender, AppValues.MailPass);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                    Console.WriteLine("Письмо успешно отправлено!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
            }
        }
    }
}

