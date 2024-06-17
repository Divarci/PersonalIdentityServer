namespace EntityLayer.Models.DTOs.IdentityResourceDto
{
    public class IdentityResourceListDto
    {
        public List<IdentityResourceItemDto> IdentityResources { get; set; }
        public bool HasOpenid { get; set; }
    }
}
