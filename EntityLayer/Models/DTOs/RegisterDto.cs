namespace EntityLayer.Models.DTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }

    }
}
