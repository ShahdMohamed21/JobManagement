namespace JobManagement.Application.DTOs.Users;

public class UserResponse
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? CVUrl { get; set; }
}