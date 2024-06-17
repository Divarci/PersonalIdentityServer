namespace EntityLayer.Models.DTOs.AuthenticationDto
{
    public class UserDtoForAdmin
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public string Role { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}
