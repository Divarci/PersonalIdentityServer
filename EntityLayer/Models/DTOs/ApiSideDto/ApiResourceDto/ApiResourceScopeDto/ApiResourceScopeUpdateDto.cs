namespace EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto
{
    public class ApiResourceScopeUpdateDto
    {
        public int Id { get; set; }
        public string Scope { get; set; }
        public int ApiResourceId { get; set; }
    }
}
