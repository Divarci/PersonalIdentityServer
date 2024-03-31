using EntityLayer.Models.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Models.DTOs
{
    [ModelMetadataType(typeof(EmailServiceMetadata))]
    public class EmailServiceInfo
    {
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
