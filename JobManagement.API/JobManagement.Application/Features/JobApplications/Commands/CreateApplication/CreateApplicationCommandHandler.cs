using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Commands.CreateApplication;

public class CreateApplicationCommandHandler
    : IRequestHandler<CreateApplicationCommand, JobApplicationResponse?>
{
    private readonly IJobApplicationRepository _jobApplicationRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IBackgroundJobService _backgroundJobService;

    public CreateApplicationCommandHandler(
        IJobApplicationRepository jobApplicationRepository,
        IJobRepository jobRepository,
        IBackgroundJobService backgroundJobService)
    {
        _jobApplicationRepository = jobApplicationRepository;
        _jobRepository = jobRepository;
        _backgroundJobService = backgroundJobService;
    }

    public async Task<JobApplicationResponse?> Handle(
        CreateApplicationCommand request,
        CancellationToken cancellationToken)
    {
        var application =
            await _jobApplicationRepository.CreateApplicationAsync(
                request.JobId,
                request.CandidateId);

        if (application == null)
            return null;

        var recruiterId =
            await _jobRepository.GetRecruiterIdByJobIdAsync(
                request.JobId);

        if (recruiterId != null)
        {
            _backgroundJobService.EnqueueApplicationNotification( recruiterId,request.CandidateId,request.JobId);
        }

        return application;
    }
}