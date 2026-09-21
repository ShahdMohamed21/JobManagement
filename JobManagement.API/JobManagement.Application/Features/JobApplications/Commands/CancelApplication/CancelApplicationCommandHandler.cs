using JobManagement.Application.Features.Jobs.Commands.CancelJob;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.JobApplications.Commands.CancelApplication
{
   public class CancelApplicationCommandHandler:IRequestHandler<CancelApplicationCommand,bool>
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;
        public CancelApplicationCommandHandler(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }
        public async Task<bool> Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
        {
            return await _jobApplicationRepository.CancelApplicationAsync(request.ApplicationId, request.CandidateId);
        }
    }
}
