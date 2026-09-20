using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Application.DTOs.Jobs
{
    public class CreateJobRequest
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
