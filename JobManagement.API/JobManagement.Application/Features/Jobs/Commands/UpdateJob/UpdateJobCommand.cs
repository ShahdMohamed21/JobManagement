using JobManagement.Application.DTOs.Jobs;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Commands.UpdateJob;

public record UpdateJobCommand(int JobId, string Title, string Description,string RecruiterId) : IRequest<JobResponse?>;