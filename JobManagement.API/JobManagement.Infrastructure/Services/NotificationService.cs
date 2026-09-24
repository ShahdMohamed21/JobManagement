using JobManagement.Application.Interfaces;
using JobManagement.Application.Interfaces.Repositories;

namespace JobManagement.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService( INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task SendApplicationNotificationAsync(string recruiterId, string candidateId,int jobId)
    {
        var message =$"Candidate {candidateId} applied for Job {jobId}";

        await _notificationRepository.CreateAsync( recruiterId,message);
    }
}