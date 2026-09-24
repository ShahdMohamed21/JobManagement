using JobManagement.Application.DTOs.Jobs;
using MediatR;

namespace JobManagement.Application.Features.Jobs.Queries.GetJobById;

public record GetJobByIdQuery(int JobId) : IRequest<JobResponse?>;