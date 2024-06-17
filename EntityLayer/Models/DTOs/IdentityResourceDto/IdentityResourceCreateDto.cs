using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;

namespace EntityLayer.Models.DTOs.IdentityResourceDto
{
    public class IdentityResourceCreateDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
    }
}
