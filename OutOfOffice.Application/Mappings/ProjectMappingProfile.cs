using AutoMapper;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Mappings
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectDto, OutOfOffice.Domain.Entities.Project>();
            CreateMap<OutOfOffice.Domain.Entities.Project, GetProjectDto>()
                .ForMember(p => p.ProjectManagerName, opt => opt.MapFrom(src =>
                src.EmployeeProjects
                .Where(ep => ep.Employee.Position == "Project Manager")
                .Select(ep => ep.Employee.FullName)
                .FirstOrDefault()));
        }
    }
}
