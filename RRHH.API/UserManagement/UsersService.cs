using Microsoft.AspNetCore.Identity;
using RRHH.API.Data;
using RRHH.API.Data.Entities;
using RRHH.API.DTOs;

namespace RRHH.API.UserManagement;

public sealed class UsersService
{
    private readonly DataContext _dataContext;
    private readonly UserManager<AplicationUser> _userManager;
    private readonly SignInManager<AplicationUser> _signInManager;

    public UsersService(DataContext dataContext, UserManager<AplicationUser> userManager, SignInManager<AplicationUser> signInManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    internal async Task<UserDTO> Login(string userName, string password)
    {
        var dbUser = await _userManager.FindByEmailAsync(userName);
        if (dbUser is null) return null;

        var signinResult = await _signInManager.PasswordSignInAsync(dbUser, password, isPersistent: true, lockoutOnFailure: false);
        if (!signinResult.Succeeded) return null;

        var userRoles = await _userManager.GetRolesAsync(dbUser);

        return new UserDTO
        {
            Id = dbUser.Id,
            FullName = dbUser.FullName,
            Email = dbUser.Email,
            Role = userRoles.FirstOrDefault() ?? "RH"
        };
    }
}