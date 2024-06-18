using AutoMapper;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Domain.Models;

namespace OutOfOffice.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<UserRoleDto> GetViewDataAsync()
        {
            var userDtos = await GetAllUsersWithRolesAsync();
            var roleDtos = await GetAllRolesAsync();

            var userRoleDto = new UserRoleDto()
            {
                UserDtos = userDtos,
                RoleDtos = roleDtos
            };

            return userRoleDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync()
        {
            var userDtos = await _userRoleRepository.GetAllUsersWithRolesAsync();

            return userDtos;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var identityRoles = await _userRoleRepository.GetAllRolesAsync();

            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(identityRoles);

            return roleDtos;
        }

        public async Task UpdateUserRoleAsync(UserRoleDto userRoleDto)
        {
            await _userRoleRepository.UpdateUserRoleAsync(userRoleDto);
        }

    }
}
