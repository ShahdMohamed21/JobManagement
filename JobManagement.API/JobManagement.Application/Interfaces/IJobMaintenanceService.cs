namespace JobManagement.Application.Interfaces;

public interface IJobMaintenanceService
{
    Task CloseExpiredJobsAsync();
}