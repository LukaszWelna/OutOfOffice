using AutoMapper;
using OutOfOffice.Application.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<CreateEmployeeDto, OutOfOffice.Domain.Entities.Employee>();
            CreateMap<OutOfOffice.Domain.Entities.Employee, GetEmployeeDto>()
                .ForMember(e => e.PeoplePartner, opt => opt.MapFrom(src => src.PeoplePartner != null ? src.PeoplePartner.FullName : null));
            CreateMap<OutOfOffice.Domain.Entities.Employee, EditEmployeeDto>()
                .ForMember(e => e.SubdivisionId, opt => opt.MapFrom(src =>
                SubdivisionList.Subdivisions.FirstOrDefault(s => s.Name == src.Subdivision)!.Id))
                .ForMember(e => e.PositionId, opt => opt.MapFrom(src =>
                PositionList.Positions.FirstOrDefault(p => p.Name == src.Position)!.Id));
        }
    }
}
