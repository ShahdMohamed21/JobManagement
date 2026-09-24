using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Commands.CancelApplication;

public class CancelApplicationCommandHandler: IRequestHandler<CancelApplicationCommand, bool>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;

    public CancelApplicationCommandHandler( IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }

    public async Task<bool> Handle(CancelApplicationCommand request, CancellationToken cancellationToken)
    {
        return await _jobApplicationRepository.CancelApplicationAsync( request.ApplicationId, request.CandidateId);
    }
}