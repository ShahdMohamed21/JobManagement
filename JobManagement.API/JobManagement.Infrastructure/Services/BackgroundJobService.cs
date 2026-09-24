using Hangfire;
using JobManagement.Application.Interfaces;

namespace JobManagement.Infrastructure.Services;

public class BackgroundJobService : IBackgroundJobService
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public BackgroundJobService(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public void EnqueueApplicationNotification(
        string recruiterId,
        string candidateId,
        int jobId)
    {
        _backgroundJobClient.Enqueue<INotificationService>(
            service => service.SendApplicationNotificationAsync(
                recruiterId,
                candidateId,
                jobId));
    }
}