namespace EntityLayer.Models.DTOs
{
    public class PasswordUpdateDto
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
