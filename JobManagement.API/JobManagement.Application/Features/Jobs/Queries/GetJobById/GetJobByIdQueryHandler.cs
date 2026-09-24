using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Queries.GetJobById;

public class GetJobByIdQueryHandler: IRequestHandler<GetJobByIdQuery, JobResponse?>
{
    private readonly IJobRepository _jobRepository;

    public GetJobByIdQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobResponse?> Handle(
        GetJobByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _jobRepository.GetJobByIdAsync(request.JobId);
    }
}