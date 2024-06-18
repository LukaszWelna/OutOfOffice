using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OutOfOffice.Application.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
