using JobManagement.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.Jobs.Commands.CancelJob
{
    public class CancelJobCommandHandler : IRequestHandler<CancelJobCommand, bool>
    {
        private readonly IJobRepository _jobRepository;

        public CancelJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<bool> Handle(CancelJobCommand request, CancellationToken cancellationToken)
        {
            return await _jobRepository.CancelJobAsync(request.JobId, request.RecruiterId);
        }
    }
}
