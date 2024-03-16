using EntityLayer.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerApi
{
    public static class DataAdd
    {

        public static async Task AddData(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager)
        {
            await _userManager.CreateAsync(
                new AppUser()
                {
                    UserName = "hasan",
                    Email = "hasan@gmail.com",
                    ClientId = "CW",
                    FullName = "hasan divarci"
                }, "Password12*"
                );

            await _userManager.CreateAsync(
               new AppUser()
               {
                   UserName = "begum",
                   Email = "begum@gmail.com",
                   ClientId = "CW",
                   FullName = "begum divarci"
               }, "Password12*"
               );

            await _roleManager.CreateAsync(new AppRole()
            {
                Name = "Admin"
            });
            await _roleManager.CreateAsync(new AppRole()
            {
                Name = "Member"
            });

            var userHasan = await _userManager.FindByNameAsync("hasan");
            var userBegum = await _userManager.FindByNameAsync("begum");

            var adminRole = await _roleManager.FindByNameAsync("admin");
            var memberRole = await _roleManager.FindByNameAsync("member");

            await _userManager.AddToRoleAsync(userHasan, "Admin");
            await _userManager.AddToRoleAsync(userBegum, "Member");
        }

    }
}
