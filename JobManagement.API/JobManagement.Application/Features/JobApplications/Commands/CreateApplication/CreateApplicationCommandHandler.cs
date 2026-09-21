using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.JobApplications.Commands.CreateApplication
{
    public class CreateApplicationCommandHandler: IRequestHandler<CreateApplicationCommand, JobApplicationResponse?>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public CreateApplicationCommandHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }
        public async Task<JobApplicationResponse?> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _jobApplicationRepository.CreateApplicationAsync(request.JobId, request.CandidateId);
        }
    }
}
