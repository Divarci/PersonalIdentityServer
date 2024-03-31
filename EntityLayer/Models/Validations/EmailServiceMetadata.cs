using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models.Validations
{
    public class EmailServiceMetadata
    {
        [EmailAddress(ErrorMessage = "Please use a valid email type.")]
        public string Email { get; set; }
       
    }
}
