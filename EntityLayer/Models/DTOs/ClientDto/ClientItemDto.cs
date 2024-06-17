using Duende.IdentityServer.Models;
using EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientScopeDto;
using EntityLayer.Models.DTOs.ClientDto.ClientSecretDto;

namespace EntityLayer.Models.DTOs.ClientDto
{
    public class ClientItemDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public List<ClientGrantTypeItemDto> AllowedGrantTypes { get; set; }
        public bool HasClientGrantType { get; set; }
        public List<ClientSecretItemDto> ClientSecrets { get; set; }
        public bool HasClientSecret { get; set; }
        public List<ClientScopeItemDto> AllowedScopes { get; set; }
        public bool HasClientScope { get; set; }
        public int AccessTokenLifetime { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public TokenUsage RefreshTokenUsage { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }

    }
}
