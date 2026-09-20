using JobManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Domain.Entities
{
    public class JobApplication
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public string CandidateId { get; set; } = null!;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        public DateTime AppliedAt { get; set; }
    }
}
