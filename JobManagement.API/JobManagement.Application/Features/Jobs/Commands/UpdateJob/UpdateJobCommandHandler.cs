using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Commands.UpdateJob;

public class UpdateJobCommandHandler: IRequestHandler<UpdateJobCommand, JobResponse?>
{
    private readonly IJobRepository _jobRepository;

    public UpdateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
    {
        return await _jobRepository.UpdateJobAsync(request);
    }
}