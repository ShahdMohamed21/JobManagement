using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Domain.Enums;
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
        Task<List<JobApplicationResponse>> GetMyApplicationsAsync(string candidateId);
        Task<List<JobApplicationResponse>> GetRecruiterApplicationsAsync(string recruiterId);
        Task<bool> UpdateApplicationStatusAsync(int applicationId, string recruiterId, ApplicationStatus status);
    }
}
