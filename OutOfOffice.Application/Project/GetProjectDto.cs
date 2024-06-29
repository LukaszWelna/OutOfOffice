using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Project
{
    public class GetProjectDto
    {
        public int Id { get; set; }
        public string ProjectType { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string ProjectManagerName { get; set; } = default!;
        public string? Comment { get; set; }
        public string Status { get; set; } = default!;
    }
}
