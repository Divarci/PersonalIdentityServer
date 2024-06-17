using Duende.IdentityServer.EntityFramework.Entities;
using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;

namespace EntityLayer.Models.DTOs.IdentityResourceDto
{
    public class IdentityResourceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }      
        public bool Required { get; set; }
        public List<IdentityResourceClaimItemDto> UserClaims { get; set; }
        public bool HasOpenid { get; set; }
    }
}
