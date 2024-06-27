using AutoMapper;
using OutOfOffice.Application.ApprovalRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Mappings
{
    public class ApprovalRequestMappingProfile : Profile
    {
        public ApprovalRequestMappingProfile()
        {
            CreateMap<OutOfOffice.Domain.Entities.ApprovalRequest, GetApprovalRequestDto>()
                .ForMember(ar => ar.ApproverName, opt => opt.MapFrom(src => src.Approver != null ? src.Approver.FullName : ""));
        }
    }
}
