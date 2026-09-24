using JobManagement.Application.DTOs.JobApplications;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Queries.GetMyApplications;

public record GetMyApplicationsQuery(string CandidateId) : IRequest<List<JobApplicationResponse>>;