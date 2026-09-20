using JobManagement.Application.DTOs.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Interfaces
{
    public interface IJobService
    {
        Task<List<JobResponse>> GetAllJobsAsync();
        Task<JobResponse> CreateJobAsync(CreateJobRequest request, string recruiterId);
        Task<bool> SoftDeleteJobAsync(int jobId, string recruiterId);
    }
}
