using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Admin.Commands.UpdateUserStatus;

public class UpdateUserStatusCommandHandler: IRequestHandler<UpdateUserStatusCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserStatusCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle( UpdateUserStatusCommand request,CancellationToken cancellationToken)
    {
        return await _userRepository.UpdateUserStatusAsync(request.UserId, request.IsActive);
    }
}