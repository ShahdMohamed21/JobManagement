namespace JobManagement.Application.Interfaces;

public interface IBackgroundJobService
{
    void EnqueueApplicationNotification(string recruiterId,string candidateId,int jobId);
}