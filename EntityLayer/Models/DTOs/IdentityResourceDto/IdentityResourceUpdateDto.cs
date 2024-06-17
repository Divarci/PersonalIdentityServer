using EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto;

namespace EntityLayer.Models.DTOs.IdentityResourceDto
{
    public class IdentityResourceUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Required { get; set; }
    }
}
