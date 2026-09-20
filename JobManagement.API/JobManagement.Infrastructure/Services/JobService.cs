using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Entities;
using JobManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Services;

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;

    public JobService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<JobResponse>> GetAllJobsAsync()
    {
        return await _context.Jobs
            .AsNoTracking()
            .Where(j => j.IsActive)
            .Select(j => new JobResponse
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                IsActive = j.IsActive,
                CreatedAt = j.CreatedAt
            })
            .ToListAsync();
    }
    public async Task<JobResponse> CreateJobAsync(
    CreateJobRequest request,
    string recruiterId)
    {
        var job = new Job
        {
            Title = request.Title,
            Description = request.Description,
            RecruiterId = recruiterId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Jobs.Add(job);

        await _context.SaveChangesAsync();

        return new JobResponse
        {
            Id = job.Id,
            Title = job.Title,
            Description = job.Description,
            IsActive = job.IsActive,
            CreatedAt = job.CreatedAt
        };
    }
    public async Task<bool> SoftDeleteJobAsync(
    int jobId,
    string recruiterId)
    {
        var job = await _context.Jobs
            .FirstOrDefaultAsync(j =>
                j.Id == jobId &&
                j.RecruiterId == recruiterId &&
                j.IsActive);

        if (job == null)
            return false;

        job.IsActive = false;

        await _context.SaveChangesAsync();

        return true;
    }


}