using EntityLayer.Models.DTOs;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ServiceLayer.Helpers.EmailSender
{
    public interface IEmailHelper
    {
        Task SendEmailWithTokenForResetPasswordAsync(string emailTo,string passwordResetLink);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly EmailServiceInfoDto _emailInfo;

        public EmailHelper(IOptions<EmailServiceInfoDto> emailInfo)
        {
            _emailInfo = emailInfo.Value;
        }

        public async Task SendEmailWithTokenForResetPasswordAsync(string emailTo, string passwordResetLink)
        {
            var smtpClient = new SmtpClient();

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = _emailInfo.Host;
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials =new NetworkCredential(_emailInfo.Email,_emailInfo.Password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailInfo.Email);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = "Password Reset Link | Test Purpose";
            mailMessage.Body = $@"<h1>PASSWORD RESET LINK</h1>
                                  <h5>Click <a href='{passwordResetLink}'>HERE</a> to reset your password</h5>";
            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync(mailMessage);




        }
    }
}
