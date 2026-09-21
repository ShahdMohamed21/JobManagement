using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Features.JobApplications.Commands.CancelApplication;
using JobManagement.Application.Features.JobApplications.Commands.CreateApplication;
using JobManagement.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Candidate")]
public class JobApplicationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobApplicationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Apply(
    CreateApplicationRequest request)
    {
        var candidateId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (candidateId == null)
            return Unauthorized();

        var command = new CreateApplicationCommand( request.JobId, candidateId);

        var application = await _mediator.Send(command);

        if (application == null)
            return BadRequest("Job is not available or you already applied");

        return Ok(application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelApplication(int id)
    {
        var candidateId =User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (candidateId == null)
            return Unauthorized();

        var command=new CancelApplicationCommand(id, candidateId);
        var cancelled=await _mediator.Send(command);

        if (!cancelled)
            return NotFound();

        return NoContent();
    }
}