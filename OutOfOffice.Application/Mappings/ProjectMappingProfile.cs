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
        }
    }
}
