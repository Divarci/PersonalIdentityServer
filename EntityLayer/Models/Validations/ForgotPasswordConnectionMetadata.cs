using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Validations
{
    public class ForgotPasswordConnectionMetadata
    {
        [EmailAddress(ErrorMessage ="Please use a valid email type.")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
       
    }
}
