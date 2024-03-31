namespace EntityLayer.Models.DTOs
{
    public class ForgotPasswordInfo
    {
        public EmailServiceInfo EmailService { get; set; }
        public ForgotPasswordConnection ForgotPasswordConnection { get; set; }
    }
}
