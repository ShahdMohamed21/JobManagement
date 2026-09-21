using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Queries.GetAllJobs;

public class GetAllJobsQueryHandler: IRequestHandler<GetAllJobsQuery, List<JobResponse>>
{
    private readonly IJobRepository _jobRepository;

    public GetAllJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<List<JobResponse>> Handle(GetAllJobsQuery request,CancellationToken cancellationToken)
    {
        return await _jobRepository.GetAllActiveJobsAsync();
    }
}