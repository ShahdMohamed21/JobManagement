using JobManagement.Application.Interfaces;
using JobManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Services;

public class JobMaintenanceService : IJobMaintenanceService
{
    private readonly ApplicationDbContext _context;

    public JobMaintenanceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CloseExpiredJobsAsync()
    {
        var expiredJobs = await _context.Jobs
            .Where(j =>
                j.IsActive &&
                j.ExpiryDate.HasValue &&
                j.ExpiryDate.Value <= DateTime.UtcNow)
            .ToListAsync();

        foreach (var job in expiredJobs)
        {
            job.IsActive = false;
        }

        await _context.SaveChangesAsync();
    }
}