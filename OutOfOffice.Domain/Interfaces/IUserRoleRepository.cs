using Microsoft.AspNetCore.Identity;
using OutOfOffice.Domain.Models;

namespace OutOfOffice.Domain.Interfaces
{
    public interface IUserRoleRepository
    {
        public Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync();
        public Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
        public Task UpdateUserRoleAsync(UserRoleDto userRoleDto);
        public Task<string> GetRoleByUserIdAsync(string userId);
    }
}
