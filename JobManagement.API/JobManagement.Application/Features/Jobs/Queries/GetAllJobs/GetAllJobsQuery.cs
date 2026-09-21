using JobManagement.Application.DTOs.Jobs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.Jobs.Queries.GetAllJobs
{
    public record GetAllJobsQuery : IRequest<List<JobResponse>>
    {
    }
}
