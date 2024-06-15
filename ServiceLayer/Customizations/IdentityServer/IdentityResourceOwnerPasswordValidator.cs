using Duende.IdentityServer.Validation;
using EntityLayer.Models.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Constants;

namespace ServiceLayer.Customizations.IdentityServer
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByNameAsync(context.UserName);
            if (user == null)
            {
                return;
            }           
            var passwordCheck = await _userManager.CheckPasswordAsync(user, context.Password);
            if (!passwordCheck)
            {
                return;
            }

            context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.GrantTypes.Password);
        }
    }
}
