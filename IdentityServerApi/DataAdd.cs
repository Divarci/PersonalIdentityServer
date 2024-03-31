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
                    UserName = "veli",
                    Email = "veli@gmail.com",
                    ClientId = "CW",
                    FullName = "veli donmez"
                }, "Password12*"
                );

            await _userManager.CreateAsync(
               new AppUser()
               {
                   UserName = "begum",
                   Email = "begum@gmail.com",
                   ClientId = "Test",
                   FullName = "begum divarci"
               }, "Password12*"
               );
            await _userManager.CreateAsync(
               new AppUser()
               {
                   UserName = "ahmet",
                   Email = "ahmet@gmail.com",
                   ClientId = "Test",
                   FullName = "ahmet yorulmaz"
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
            var userVeli = await _userManager.FindByNameAsync("veli");
            var userAhmet = await _userManager.FindByNameAsync("ahmet");

            var adminRole = await _roleManager.FindByNameAsync("admin");
            var memberRole = await _roleManager.FindByNameAsync("member");

            await _userManager.AddToRoleAsync(userHasan, "Admin");
            await _userManager.AddToRoleAsync(userBegum, "Admin");
            await _userManager.AddToRoleAsync(userVeli, "Member");
            await _userManager.AddToRoleAsync(userAhmet, "Member");
        }

    }
}
