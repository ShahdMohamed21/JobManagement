using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Entities;
using JobManagement.Domain.Enums;
using JobManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobManagement.Infrastructure.Services;

public class JobApplicationService : IJobApplicationService
{
    private readonly ApplicationDbContext _context;

    public JobApplicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<JobApplicationResponse?> CreateApplicationAsync(
        CreateApplicationRequest request,
        string candidateId)
    {
        var job = await _context.Jobs
            .FirstOrDefaultAsync(j =>
                j.Id == request.JobId &&
                j.IsActive);

        if (job == null)
            return null;

        var existingApplication = await _context.JobApplications
            .AnyAsync(a =>
                a.JobId == request.JobId &&
                a.CandidateId == candidateId &&
                a.Status != ApplicationStatus.Cancelled);

        if (existingApplication)
            return null;

        var application = new JobApplication
        {
            JobId = request.JobId,
            CandidateId = candidateId,
            Status = ApplicationStatus.Applied,
            AppliedAt = DateTime.UtcNow
        };

        _context.JobApplications.Add(application);

        await _context.SaveChangesAsync();

        return new JobApplicationResponse
        {
            Id = application.Id,
            JobId = application.JobId,
            CandidateId = application.CandidateId,
            Status = application.Status.ToString(),
            AppliedAt = application.AppliedAt
        };
    }

    public async Task<bool> CancelApplicationAsync(
        int applicationId,
        string candidateId)
    {
        var application = await _context.JobApplications
            .FirstOrDefaultAsync(a =>
                a.Id == applicationId &&
                a.CandidateId == candidateId);

        if (application == null)
            return false;

        if (application.Status == ApplicationStatus.Cancelled)
            return false;

        application.Status = ApplicationStatus.Cancelled;

        await _context.SaveChangesAsync();

        return true;
    }
}