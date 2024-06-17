using EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto.ApiResourceScopeDto;

namespace EntityLayer.Models.DTOs.ApiSideDto.ApiResourceDto
{
    public class ApiResourceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<ApiResourceScopeItemDto> Scopes { get; set; }
        public bool IsScopeAssigned { get; set; }
        public bool IsScopesHavePerfectMatch { get; set; }
    }
}
