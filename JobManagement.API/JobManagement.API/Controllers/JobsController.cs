using JobManagement.Application.DTOs.Jobs;
using JobManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobs = await _jobService.GetAllJobsAsync();

        return Ok(jobs);
    }
    [HttpPost]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> CreateJob(CreateJobRequest request)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var job = await _jobService.CreateJobAsync(request, recruiterId);

        return Ok(job);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Recruiter")]
    public async Task<IActionResult> DeleteJob(int id)
    {
        var recruiterId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (recruiterId == null)
            return Unauthorized();

        var deleted = await _jobService.SoftDeleteJobAsync(id, recruiterId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}