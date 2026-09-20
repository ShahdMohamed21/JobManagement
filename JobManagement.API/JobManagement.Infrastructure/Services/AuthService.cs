using JobManagement.Application.DTOs.Auth;
using JobManagement.Application.Interfaces;
using JobManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobManagement.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager , ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var allowedRoles = new[] { "Candidate", "Recruiter" };

        if (!allowedRoles.Contains( request.Role, StringComparer.OrdinalIgnoreCase))
        {
            return false;
        }

        var role = allowedRoles.First(r => r.Equals(request.Role, StringComparison.OrdinalIgnoreCase));

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            CVUrl = request.CVUrl
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return false;
        }

        var roleResult = await _userManager.AddToRoleAsync(
            user,
            role);

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            return false;
        }

        return true;
    }
    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return null;
        }

        var passwordValid = await _userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!passwordValid)
        {
            return null;
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = await _tokenService.GenerateTokenAsync(
            user.Id,
            user.Email!,
            roles);

        return token;
    }
}