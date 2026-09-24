using JobManagement.Application.DTOs.Users;
using JobManagement.Application.Features.Admin.Commands.UpdateUserStatus;
using JobManagement.Application.Features.Admin.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _mediator.Send(new GetUsersQuery());

        return Ok(users);
    }

    [HttpPut("users/{id}/status")]
    public async Task<IActionResult> UpdateUserStatus(string id,UpdateUserStatusRequest request)
    {
        var command = new UpdateUserStatusCommand(
            id,
            request.IsActive);

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }
}