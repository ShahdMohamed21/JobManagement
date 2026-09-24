using JobManagement.Application.DTOs.Users;
using JobManagement.Application.Interfaces.Repositories;
using JobManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();

        var result = new List<UserResponse>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            result.Add(new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName!,
                Email = user.Email!,
                Role = roles.FirstOrDefault() ?? "No Role",
                CVUrl = user.CVUrl
            });
        }

        return result;
    }
    public async Task<bool> UpdateUserStatusAsync(string userId,bool isActive)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return false;

        user.IsActive = isActive;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}