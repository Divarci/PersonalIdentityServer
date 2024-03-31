using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ServiceLayer.Exceptions
{
    public static class CustomExceptionMiddleware
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeatures = context.Features.Get<IExceptionHandlerFeature>();
                    var statuscode = exceptionFeatures.Error switch
                    {                       
                        _ => 500
                    };

                    context.Response.StatusCode = statuscode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statuscode, exceptionFeatures.Error.Message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });


        }
    }
}
