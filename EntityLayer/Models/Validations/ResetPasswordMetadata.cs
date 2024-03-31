using EntityLayer.Messages;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models.Validations
{
    public class ResetPasswordMetadata
    {
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = CustomErrorMessages.PasswordNotMatch)]
        public string PasswordConfirm { get; set; }
    }
}
