using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Commands.DeleteJob;

public class DeleteJobCommandHandler: IRequestHandler<DeleteJobCommand, bool>
{
    private readonly IJobRepository _jobRepository;

    public DeleteJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        return await _jobRepository.DeleteJobAsync(request.JobId,request.RecruiterId);
    }
}