using EntityLayer.Messages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EntityLayer.Models.Validations
{
    public class RegisterMetadata
    {
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = CustomErrorMessages.PasswordNotMatch)]
        public string ConfirmPassword { get; set; }
    }
}
