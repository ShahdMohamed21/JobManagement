using JobManagement.Application.DTOs.Users;

namespace JobManagement.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<UserResponse>> GetAllUsersAsync();
    Task<bool> UpdateUserStatusAsync(string userId,bool isActive);
}