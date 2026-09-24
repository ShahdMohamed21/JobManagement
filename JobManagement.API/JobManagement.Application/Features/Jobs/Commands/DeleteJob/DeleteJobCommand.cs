using MediatR;

namespace JobManagement.Application.Features.Jobs.Commands.DeleteJob;

public record DeleteJobCommand(int JobId,string RecruiterId) : IRequest<bool>;