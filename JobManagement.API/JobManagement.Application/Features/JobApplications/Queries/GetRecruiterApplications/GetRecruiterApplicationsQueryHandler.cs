using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Queries.GetRecruiterApplications;

public class GetRecruiterApplicationsQueryHandler: IRequestHandler<GetRecruiterApplicationsQuery, List<JobApplicationResponse>>
{
    private readonly IJobApplicationRepository _repository;

    public GetRecruiterApplicationsQueryHandler(
        IJobApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<JobApplicationResponse>> Handle(GetRecruiterApplicationsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetRecruiterApplicationsAsync(request.RecruiterId);
    }
}