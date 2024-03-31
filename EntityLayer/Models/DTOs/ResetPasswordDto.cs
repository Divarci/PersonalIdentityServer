using EntityLayer.Models.Validations;
using Microsoft.AspNetCore.Mvc;

namespace EntityLayer.Models.DTOs
{
    [ModelMetadataType(typeof(ResetPasswordMetadata))]
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
