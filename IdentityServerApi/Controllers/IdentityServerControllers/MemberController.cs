using EntityLayer.Models.DTOs.AuthenticationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Constants;
using ServiceLayer.Services.MemberServices;

namespace IdentityServerApi.Controllers.IdentityServerControllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(CustomIdentityConstants.MemberRole)]
    [ApiController]
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var result = await _memberService.GetUserAsync(User);
            return CreateAction(result);

        }
        [HttpPut]
        public async Task<IActionResult> UserUpdate(UserDtoForMember request)
        {
            var result = await _memberService.UserUpdateAsync(request, User);
            return CreateAction(result);
        }

        [HttpPut]
        public async Task<IActionResult> PasswordChange(PasswordUpdateDto request)
        {
            var result = await _memberService.PasswordChangeAsync(request, User);
            return CreateAction(result);
        }

    }
}
