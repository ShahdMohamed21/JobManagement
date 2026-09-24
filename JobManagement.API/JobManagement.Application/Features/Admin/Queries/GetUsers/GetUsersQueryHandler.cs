using JobManagement.Application.DTOs.Users;
using JobManagement.Application.Interfaces.Repositories;
using MediatR;

namespace JobManagement.Application.Features.Admin.Queries.GetUsers;

public class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, List<UserResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllUsersAsync();
    }
}