using MediatR;

namespace JobManagement.Application.Features.Admin.Commands.UpdateUserStatus;

public record UpdateUserStatusCommand(string UserId,bool IsActive) : IRequest<bool>;