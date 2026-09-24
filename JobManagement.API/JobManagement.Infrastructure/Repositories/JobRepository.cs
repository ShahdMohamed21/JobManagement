using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Features.Jobs.Commands.CreateJob;
using JobManagement.Application.Features.Jobs.Commands.UpdateJob;
using JobManagement.Application.Interfaces.Repositories;
using JobManagement.Domain.Entities;
using JobManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Infrastructure.Repositories
{
    public class JobRepository: IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<JobResponse>> GetAllActiveJobsAsync()
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

        public async Task<JobResponse?> CreateJobAsync(CreateJobCommand request)
        {
            var job = new Job
            {
                Title = request.Title,
                Description = request.Description,
                RecruiterId = request.RecruiterId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = request.ExpiryDate
            };

            await _context.Jobs.AddAsync(job);
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
        public async Task<bool> CancelJobAsync(int jobId, string recruiterId)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j =>
                    j.Id == jobId &&
                    j.RecruiterId == recruiterId &&
                    j.IsActive);

            if (job == null)
                return false;

            job.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<JobResponse?> GetJobByIdAsync(int jobId)
        {
            return await _context.Jobs
                .AsNoTracking()
                .Where(j => j.Id == jobId && j.IsActive)
                .Select(j => new JobResponse
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    IsActive = j.IsActive,
                    CreatedAt = j.CreatedAt
                })
                .FirstOrDefaultAsync();
        }
        public async Task<JobResponse?> UpdateJobAsync(UpdateJobCommand request)
        {
            var job = await _context.Jobs
                .FirstOrDefaultAsync(j =>
                    j.Id == request.JobId &&
                    j.RecruiterId == request.RecruiterId &&
                    j.IsActive);

            if (job == null)
                return null;

            job.Title = request.Title;
            job.Description = request.Description;

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
        public async Task<bool> DeleteJobAsync(int jobId,string recruiterId)
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
        public async Task<string?> GetRecruiterIdByJobIdAsync(int jobId)
        {
            return await _context.Jobs
                .Where(j => j.Id == jobId && j.IsActive)
                .Select(j => j.RecruiterId)
                .FirstOrDefaultAsync();
        }
    }
}
