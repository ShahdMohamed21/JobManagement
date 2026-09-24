namespace JobManagement.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task CreateAsync( string userId, string message);
}