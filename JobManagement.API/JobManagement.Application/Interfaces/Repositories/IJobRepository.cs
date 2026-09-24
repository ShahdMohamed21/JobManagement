using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Features.Jobs.Commands.CreateJob;
using JobManagement.Application.Features.Jobs.Commands.UpdateJob;
using JobManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Interfaces.Repositories
{
    public interface IJobRepository
    {
        Task<List<JobResponse>> GetAllActiveJobsAsync();
        Task<JobResponse?> CreateJobAsync(CreateJobCommand request);
        Task<JobResponse?> GetJobByIdAsync(int jobId);
        Task<JobResponse?> UpdateJobAsync(UpdateJobCommand request);
        Task<bool> DeleteJobAsync(int jobId, string recruiterId);
        Task<string?> GetRecruiterIdByJobIdAsync(int jobId);
    }
}
