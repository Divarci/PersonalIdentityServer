namespace EntityLayer.Models.DTOs.AuthenticationDto
{
    public class PasswordUpdateDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
