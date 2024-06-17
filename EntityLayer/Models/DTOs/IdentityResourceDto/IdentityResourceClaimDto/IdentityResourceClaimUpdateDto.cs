namespace EntityLayer.Models.DTOs.IdentityResourceDto.IdentityResourceClaimDto
{
    public class IdentityResourceClaimUpdateDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int IdentityResourceId { get; set; }
    }
}
