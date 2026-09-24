namespace JobManagement.Application.DTOs.Jobs;

public class UpdateJobRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}