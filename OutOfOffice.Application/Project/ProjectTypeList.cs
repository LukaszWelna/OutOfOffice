using OutOfOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Project
{
    public static class ProjectTypeList
    {
        public static List<ProjectType> ProjectTypes { get; set; } = new List<ProjectType>()
        {
            new ProjectType() { Id = 1, Name = "Web application" },
            new ProjectType() { Id = 2, Name = "Cybersecurity" },
            new ProjectType() { Id = 3, Name = "Database development" },
            new ProjectType() { Id = 4, Name = "ERP system" },
            new ProjectType() { Id = 5, Name = "RPA" }
        };
    }
}
