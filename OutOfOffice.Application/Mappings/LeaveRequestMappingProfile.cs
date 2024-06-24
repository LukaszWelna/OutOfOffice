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
        }
    }
}
