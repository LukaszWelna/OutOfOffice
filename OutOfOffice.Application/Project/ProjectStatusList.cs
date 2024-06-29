using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Project
{
    public static class ProjectStatusList
    {
        public static List<ProjectStatus> ProjectStatuses { get; set; } = new List<ProjectStatus>()
        {
            new ProjectStatus() { Id = 1, Name = "Active" },
            new ProjectStatus() { Id = 2, Name = "Inactive" }
        };
    }
}
