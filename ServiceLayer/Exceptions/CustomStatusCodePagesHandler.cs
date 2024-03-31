using EntityLayer.Messages;
using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Exceptions
{
    public static class CustomStatusCodePagesHandler
    {
        public static void UseCustomStatusCodePages(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(async statusCodeContext =>
            {
                switch (statusCodeContext.HttpContext.Response.StatusCode)
                {
                    case 401:
                        await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(CustomResponseDto<NoContentDto>.Fail(401, new ErrorDto(CustomErrorMessages.UnauthorizedAccess)));
                        break;
                    case 403:
                        await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(CustomResponseDto<NoContentDto>.Fail(403, new ErrorDto(CustomErrorMessages.ForbiddenAccess)));
                        break;
                    case 404:
                        await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(CustomResponseDto<NoContentDto>.Fail(404, new ErrorDto(CustomErrorMessages.PageNotFound)));
                        break;
                    default:
                        await statusCodeContext.HttpContext.Response.WriteAsJsonAsync(CustomResponseDto<NoContentDto>.Fail(500, new ErrorDto(CustomErrorMessages.InternalError)));
                        break;
                }
            });

        }
    }
}
