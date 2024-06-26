﻿using OutOfOffice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Domain.Models
{
    public class UserRoleDto
    {
        public IEnumerable<UserDto> UserDtos { get; set; } = new List<UserDto>();
        public IEnumerable<RoleDto> RoleDtos { get; set; } = new List<RoleDto>();
        public string? SelectedUserId { get; set; }
        public string? SelectedRoleId { get; set; }
    }
}
