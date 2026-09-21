using JobManagement.Application.DTOs.JobApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Interfaces.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<JobApplicationResponse?> CreateApplicationAsync(int jobId,string candidateId);
        Task<bool> CancelApplicationAsync(int ApplicationId, string CandidateId);
    }
}
