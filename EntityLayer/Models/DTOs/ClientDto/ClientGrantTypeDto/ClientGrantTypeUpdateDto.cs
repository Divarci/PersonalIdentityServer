namespace EntityLayer.Models.DTOs.ClientDto.ClientGrantTypeDto
{
    public class ClientGrantTypeUpdateDto
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public int ClientId { get; set; }
    }
}
