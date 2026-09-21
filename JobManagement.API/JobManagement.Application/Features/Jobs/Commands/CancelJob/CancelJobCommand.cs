using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.Features.Jobs.Commands.CancelJob
{
    public record CancelJobCommand(int JobId,string RecruiterId) : IRequest<bool>;
}
