using JobManagement.Application.DTOs.JobApplications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.JobApplications.Commands.CreateApplication
{
    public record CreateApplicationCommand(int JobId,string CandidateId) : IRequest<JobApplicationResponse?>;
}
