using OutOfOffice.Application.UserRole;
using OutOfOffice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Services
{
    public interface IUserRoleService
    {
        public Task<UserRoleDto> GetViewDataAsync();
        public Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync();
        public Task UpdateAsync();
    }
}
