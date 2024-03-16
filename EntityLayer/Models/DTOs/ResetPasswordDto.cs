namespace EntityLayer.Models.DTOs
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
