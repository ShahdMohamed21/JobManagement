using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.JobApplications.Commands.CancelApplication
{
    public record CancelApplicationCommand(int ApplicationId,string CandidateId):IRequest<bool>
    {
    }
}
