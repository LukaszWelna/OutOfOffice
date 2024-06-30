using AutoMapper;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;
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

            CreateMap<OutOfOffice.Domain.Entities.Project, EditProjectDto>()
                .ForMember(p => p.ProjectTypeId, opt => opt.MapFrom(src =>
                ProjectTypeList.ProjectTypes.FirstOrDefault(pt => pt.Name == src.ProjectType)!.Id))
                .ForMember(p => p.StatusId, opt => opt.MapFrom(src =>
                ProjectStatusList.ProjectStatuses.FirstOrDefault(ps => ps.Name == src.Status)!.Id))
                .ForMember(p => p.ProjectManagerId, opt => opt.MapFrom(src =>
                src.EmployeeProjects
                .Where(ep => ep.Employee.Position == "Project Manager")
                .Select(ep => ep.Employee.Id)
                .FirstOrDefault()));
        }
    }
}
