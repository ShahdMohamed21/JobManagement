using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Domain.Enums;
using JobManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Infrastructure.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobApplicationResponse?> CreateApplicationAsync(int jobId,string candidateId)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j =>
                    j.Id == jobId &&
                    j.IsActive);

            if (job == null)
                return null;

            var existingApplication = await _context.JobApplications
                .AnyAsync(a =>a.JobId == jobId && a.CandidateId == candidateId && a.Status != ApplicationStatus.Cancelled);

            if (existingApplication)
                return null;

            var application = new JobApplication
            {
                JobId = jobId,
                CandidateId = candidateId,
                Status = ApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow
            };

            await _context.JobApplications.AddAsync(application);
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

        public async Task<bool> CancelApplicationAsync(int ApplicationId, string CandidateId)
        {
            var App = await _context.JobApplications.FirstOrDefaultAsync(
                a => a.Id == ApplicationId &&
                a.CandidateId == CandidateId &&
                a.Status != ApplicationStatus.Cancelled
                );
            if (App == null) return false;
            App.Status = ApplicationStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
