using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
}
