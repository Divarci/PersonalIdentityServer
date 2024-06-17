namespace EntityLayer.Models.DTOs.ClientDto.ClientSecretDto
{
    public class ClientSecretUpdateDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ClientId { get; set; }
    }
}
