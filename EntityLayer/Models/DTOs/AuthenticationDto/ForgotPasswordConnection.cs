using EntityLayer.Models.Validations;
using Microsoft.AspNetCore.Mvc;

namespace EntityLayer.Models.DTOs.AuthenticationDto
{
    [ModelMetadataType(typeof(ForgotPasswordConnectionMetadata))]
    public class ForgotPasswordConnection
    {
        public string Email { get; set; }
        public string Url { get; set; }

    }
}
