using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Queries.GetMyApplications;

public class GetMyApplicationsQueryHandler: IRequestHandler<GetMyApplicationsQuery, List<JobApplicationResponse>>
{
    private readonly IJobApplicationRepository _repository;

    public GetMyApplicationsQueryHandler(IJobApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<JobApplicationResponse>> Handle(
        GetMyApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository
            .GetMyApplicationsAsync(request.CandidateId);
    }
}