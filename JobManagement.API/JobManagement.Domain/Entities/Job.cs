using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagement.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public string RecruiterId { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }
}
