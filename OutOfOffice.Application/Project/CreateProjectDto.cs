using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Project
{
    public class CreateProjectDto
    {
        public int Id { get; set; }
        public int ProjectTypeId { get; set; }
        public List<ProjectType> ProjectTypes { get; set; } = new List<ProjectType>();
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public List<OutOfOffice.Domain.Entities.Employee> ProjectManagers { get; set; } = new List<OutOfOffice.Domain.Entities.Employee>();
        public string? Comment { get; set; }
        public int StatusId { get; set; }
        public List<ProjectStatus> ProjectStatuses { get; set; } = new List<ProjectStatus>();
    }
}
