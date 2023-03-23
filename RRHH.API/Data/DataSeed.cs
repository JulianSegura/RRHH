using Microsoft.AspNetCore.Identity;
using RRHH.API.Data.Entities;

namespace RRHH.API.Data;

public static class DataSeed
{
    public static async Task SeedIdentityData(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        await SeedRoles(services);

        var userManager = services.GetRequiredService<UserManager<AplicationUser>>();

        var rhUser = await userManager.FindByEmailAsync("julianl.segura@gmail.com");
        if (rhUser == null)
        {
            rhUser = new AplicationUser()
            {
                Id=Guid.NewGuid().ToString(),
                FullName = "Julian Segura",
                Email = "julianl.segura@gmail.com",
                UserName = "julianl.segura@gmail.com",
                EmailConfirmed = true,
                Enabled = true,
            };

            await CreateUser(userManager, rhUser, "123456", UserRole.RH.ToString());
        };


    }

    private static async Task CreateUser(UserManager<AplicationUser> userManager, AplicationUser user, string password, string role)
    {
        try
        {
            var created = userManager.CreateAsync(user, password).Result;
            if (created.Succeeded)
            {
                var createdUser = await userManager.FindByEmailAsync(user.Email);
                //var createdUser  = await userManager.FindByIdAsync(user.Id);
                await userManager.AddToRoleAsync(user, role);
            }
        }
        catch (Exception e)
        {

            throw;
        }
        
    }


    private static async Task SeedRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        var rhRoleExists = roleManager.RoleExistsAsync(UserRole.RH.ToString()).Result;
        if (!rhRoleExists) roleManager.CreateAsync(new IdentityRole(UserRole.RH.ToString()));

        var employeeRoleExists = roleManager.RoleExistsAsync(UserRole.Employee.ToString()).Result;
        if (!employeeRoleExists) roleManager.CreateAsync(new IdentityRole(UserRole.Employee.ToString()));

    }
}
