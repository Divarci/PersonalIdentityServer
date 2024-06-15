using Duende.IdentityServer.Models;

namespace EntityLayer.Models.DTOs.ClientDto
{
    public class ClientUpdateDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }       
        public int AccessTokenLifetime { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public TokenUsage RefreshTokenUsage { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }

    }
}
