namespace JobManagement.Application.Interfaces;

public interface INotificationService
{
    Task SendApplicationNotificationAsync(string recruiterId,string candidateId,int jobId);
}