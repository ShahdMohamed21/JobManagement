using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Commands.UpdateApplicationStatus;

public class UpdateApplicationStatusCommandHandler: IRequestHandler<UpdateApplicationStatusCommand, bool>
{
    private readonly IJobApplicationRepository _repository;

    public UpdateApplicationStatusCommandHandler(IJobApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(
        UpdateApplicationStatusCommand request,
        CancellationToken cancellationToken)
    {
        return await _repository.UpdateApplicationStatusAsync(request.ApplicationId,request.RecruiterId, request.Status);
    }
}