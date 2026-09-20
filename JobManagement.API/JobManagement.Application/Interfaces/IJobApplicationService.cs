using JobManagement.Application.DTOs.JobApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Interfaces
{
    public interface IJobApplicationService
    {
        Task<JobApplicationResponse?> CreateApplicationAsync( CreateApplicationRequest request,string candidateId);

        Task<bool> CancelApplicationAsync(int applicationId, string candidateId);
    }
}
