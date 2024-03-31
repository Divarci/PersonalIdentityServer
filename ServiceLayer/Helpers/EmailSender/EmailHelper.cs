using EntityLayer.Models.DTOs;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.EmailSender
{
    public interface IEmailHelper
    {
        Task SendEmailWithTokenForResetPasswordAsync(string emailTo, string passwordResetLink, EmailServiceInfo emailService);
    }

    public class EmailHelper : IEmailHelper
    {

        public async Task SendEmailWithTokenForResetPasswordAsync(string emailTo, string passwordResetLink, EmailServiceInfo emailService)
        {
            var smtpClient = new SmtpClient();

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = emailService.Host;
            smtpClient.Port = emailService.Port;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailService.Email, emailService.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(emailService.Email);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = "Password Reset Link | Test Purpose";
            mailMessage.Body = $@"<h1>PASSWORD RESET LINK</h1>
                                  <h5>Click <a href='{passwordResetLink}'>HERE</a> to reset your password</h5>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);




        }
    }
}
