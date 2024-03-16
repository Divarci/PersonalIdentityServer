namespace EntityLayer.Models.ResponseModels
{
    public class ErrorDto
    {
        public ErrorDto(List<string>? errors)
        {
            Errors = errors;
        }

        public ErrorDto(string error)
        {
            Errors = new List<string>() {error};
        }

        public List<string>? Errors { get; set; }
    }
}
