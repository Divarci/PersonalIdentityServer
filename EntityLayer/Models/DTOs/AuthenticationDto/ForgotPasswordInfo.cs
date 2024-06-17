namespace EntityLayer.Models.DTOs.AuthenticationDto
{
    public class ForgotPasswordInfo
    {
        public EmailServiceInfo EmailService { get; set; }
        public ForgotPasswordConnection ForgotPasswordConnection { get; set; }
    }
}
