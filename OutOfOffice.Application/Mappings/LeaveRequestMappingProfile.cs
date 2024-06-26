using AutoMapper;
using OutOfOffice.Application.Employee;
using OutOfOffice.Application.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Mappings
{
    public class LeaveRequestMappingProfile : Profile
    {
        public LeaveRequestMappingProfile()
        {
            CreateMap<CreateLeaveRequestDto, OutOfOffice.Domain.Entities.LeaveRequest>();
            CreateMap<OutOfOffice.Domain.Entities.LeaveRequest, GetLeaveRequestDto>()
                .ForMember(l => l.Employee, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(l => l.EmployeeEmail, opt => opt.MapFrom(src => src.Employee.Email));
            CreateMap<OutOfOffice.Domain.Entities.LeaveRequest, EditLeaveRequestDto>()
                .ForMember(l => l.AbsenceReasonId, opt => opt.MapFrom(src =>
                AbsenceReasonList.AbsenceReasons.FirstOrDefault(a => a.Name == src.AbsenceReason)!.Id));
        }
    }
}
