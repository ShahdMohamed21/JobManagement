using JobManagement.Application.DTOs.JobApplications;
using JobManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Candidate")]
public class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _applicationService;

    public JobApplicationsController(
        IJobApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<IActionResult> Apply(
        CreateApplicationRequest request)
    {
        var candidateId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (candidateId == null)
            return Unauthorized();

        var application =
            await _applicationService.CreateApplicationAsync(
                request,
                candidateId);

        if (application == null)
            return BadRequest(
                "Job is not available or you already applied.");

        return Ok(application);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelApplication(int id)
    {
        var candidateId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (candidateId == null)
            return Unauthorized();

        var cancelled =
            await _applicationService.CancelApplicationAsync(
                id,
                candidateId);

        if (!cancelled)
            return NotFound();

        return NoContent();
    }
}