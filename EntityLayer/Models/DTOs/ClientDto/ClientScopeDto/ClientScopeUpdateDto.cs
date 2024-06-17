namespace EntityLayer.Models.DTOs.ClientDto.ClientScopeDto
{
    public class ClientScopeUpdateDto
    {
        public int Id { get; set; }
        public string Scope { get; set; }
        public int ClientId { get; set; }
    }
}
