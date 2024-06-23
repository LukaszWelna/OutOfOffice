using OutOfOffice.Domain.Models;

namespace OutOfOffice.Application.Services
{
    public interface IUserRoleService
    {
        public Task<UserRoleDto> GetViewDataAsync();
        public Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync();
        public Task UpdateUserRoleAsync(UserRoleDto userRoleDto);
        public Task<string> GetRoleByUserIdAsync(string userId);
    }
}
