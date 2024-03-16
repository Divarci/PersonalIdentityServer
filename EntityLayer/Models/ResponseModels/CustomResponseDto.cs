namespace EntityLayer.Models.ResponseModels
{
    public class CustomResponseDto<T>
    {
        public CustomResponseDto(T? data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public CustomResponseDto(int statusCode)
        {
            StatusCode = statusCode;
        }

        public CustomResponseDto(int statusCode, ErrorDto errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public CustomResponseDto(int statusCode, string error)
        {
            StatusCode = statusCode;
            Errors = new ErrorDto(error);
        }

        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public ErrorDto? Errors { get; set; }


        public static CustomResponseDto<T> Success(T? data, int statusCode) => new CustomResponseDto<T>(data, statusCode);
        public static CustomResponseDto<T> Success(int statusCode) => new CustomResponseDto<T>(statusCode);
        public static CustomResponseDto<T> Fail(int statusCode, ErrorDto errors) => new CustomResponseDto<T>(statusCode,errors);
        public static CustomResponseDto<T> Fail(int statusCode,string error) => new CustomResponseDto<T>(statusCode,error);


    }
}
