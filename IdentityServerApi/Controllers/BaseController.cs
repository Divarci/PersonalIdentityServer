using EntityLayer.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateAction<T>(CustomResponseDto<T> response)
        {
            if(response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode,
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        }
    }
}
