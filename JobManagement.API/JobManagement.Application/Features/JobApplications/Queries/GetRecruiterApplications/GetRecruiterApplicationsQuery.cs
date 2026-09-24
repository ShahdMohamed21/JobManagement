using JobManagement.Application.DTOs.JobApplications;
using MediatR;

namespace JobManagement.Application.Features.JobApplications.Queries.GetRecruiterApplications;

public record GetRecruiterApplicationsQuery(string RecruiterId) : IRequest<List<JobApplicationResponse>>;