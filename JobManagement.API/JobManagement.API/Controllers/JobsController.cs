using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Features.Jobs.Commands.CancelJob;
using JobManagement.Application.Features.Jobs.Commands.CreateJob;
using JobManagement.Application.Features.Jobs.Queries.GetAllJobs;
using JobManagement.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;
    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobs = await _mediator.Send(new GetAllJobsQuery());

        return Ok(jobs);
    }
    [HttpPost]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> CreateJob(CreateJobRequest request)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
           return Unauthorized();

        var command = new CreateJobCommand( request.Title,request.Description,recruiterId);
        var job = await _mediator.Send(command);

        return Ok(job);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> CancelJob(int id)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var command = new CancelJobCommand(id,recruiterId);

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }

}