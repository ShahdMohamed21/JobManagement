using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Features.Jobs.Commands.CreateJob;
using JobManagement.Application.Features.Jobs.Commands.DeleteJob;
using JobManagement.Application.Features.Jobs.Commands.UpdateJob;
using JobManagement.Application.Features.Jobs.Queries.GetAllJobs;
using JobManagement.Application.Features.Jobs.Queries.GetJobById;
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

        var command = new CreateJobCommand( request.Title,request.Description,recruiterId, request.ExpiryDate);
        var job = await _mediator.Send(command);

        return Ok(job);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var recruiterId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var command = new DeleteJobCommand(
            id,
            recruiterId);

        var result = await _mediator.Send(command);

        if (!result)
            return NotFound();

        return NoContent();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(int id)
    {
        var job = await _mediator.Send(new GetJobByIdQuery(id));

        if (job == null)
            return NotFound();

        return Ok(job);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> UpdateJob(
    int id,
    UpdateJobRequest request)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var command = new UpdateJobCommand( id,request.Title, request.Description, recruiterId);

        var job = await _mediator.Send(command);

        if (job == null)
            return NotFound();

        return Ok(job);
    }

}