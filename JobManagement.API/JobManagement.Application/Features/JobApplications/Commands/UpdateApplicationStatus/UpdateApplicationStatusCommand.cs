using JobManagement.Domain.Enums;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Commands.UpdateApplicationStatus;

public record UpdateApplicationStatusCommand(int ApplicationId,string RecruiterId,ApplicationStatus Status) : IRequest<bool>;