using Microsoft.AspNetCore.Identity;
using MyHomeServer.Server.Data.DbModels;

namespace Server.Data
{
    public static class SeedAdministratorRoleAndUser
    {
        internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await SeedAdministratorRole(roleManager);
            await SeedAdministratorUser(userManager);
        }
        private async static Task SeedAdministratorRole(RoleManager<IdentityRole> roleManager)
        {
            bool administratorRoleExists = await roleManager.RoleExistsAsync("Administrator");
            if (!administratorRoleExists)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }
        }
        private async static Task SeedAdministratorUser(UserManager<ApplicationUser> userManager)
        {
            bool administratorUserExists = await userManager.FindByEmailAsync("admin@example.com") != null;

            if(administratorUserExists == false)
            {
                var administratorUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@example.com"
                };
                IdentityResult identityResult = await userManager.CreateAsync(administratorUser, "Admin_123");
                if(identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(administratorUser, "Administrator");
                }
            }
        }
    }
}
