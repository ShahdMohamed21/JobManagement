using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Features.JobApplications.Commands.CancelApplication;
using JobManagement.Application.Features.JobApplications.Commands.CreateApplication;
using JobManagement.Application.Features.JobApplications.Commands.UpdateApplicationStatus;
using JobManagement.Application.Features.JobApplications.Queries.GetMyApplications;
using JobManagement.Application.Features.JobApplications.Queries.GetRecruiterApplications;
using JobManagement.Application.Interfaces;
using JobManagement.Domain.Enums;
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
    public async Task<IActionResult> Apply(CreateApplicationRequest request)
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
    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        var candidateId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (candidateId == null)
            return Unauthorized();

        var query = new GetMyApplicationsQuery(candidateId);

        var applications = await _mediator.Send(query);

        return Ok(applications);
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
    [HttpGet("recruiter")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> GetRecruiterApplications()
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var query = new GetRecruiterApplicationsQuery(recruiterId);

        var applications = await _mediator.Send(query);

        return Ok(applications);
    }
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> UpdateApplicationStatus(int id,ApplicationStatus status)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var command = new UpdateApplicationStatusCommand(
            id,
            recruiterId,
            status);

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }
}