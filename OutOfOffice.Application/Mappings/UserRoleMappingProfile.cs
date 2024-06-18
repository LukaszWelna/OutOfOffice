using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OutOfOffice.Domain.Models;

namespace OutOfOffice.Application.Mappings
{
    public class UserRoleMappingProfile : Profile
    {
        public UserRoleMappingProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
