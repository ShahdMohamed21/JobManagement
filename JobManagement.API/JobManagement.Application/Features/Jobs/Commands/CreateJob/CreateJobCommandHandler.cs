using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Features.Jobs.Queries.GetAllJobs;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.Jobs.Commands.CreateJob
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand,JobResponse?>
    {
        private readonly IJobRepository _jobRepository;
        public CreateJobCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
            
        }

        public async Task<JobResponse?> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            return await _jobRepository.CreateJobAsync(request);
        }

    }
}
