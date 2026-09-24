using JobManagement.Application.DTOs.Users;
using MediatR;

namespace JobManagement.Application.Features.Admin.Queries.GetUsers;

public record GetUsersQuery : IRequest<List<UserResponse>>;