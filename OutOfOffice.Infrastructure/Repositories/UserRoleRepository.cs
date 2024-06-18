using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Domain.Models;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager
                .Users
                .ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var role = userRoles.FirstOrDefault();

                userDtos.Add(new UserDto()
                {
                    Id = user.Id,
                    Email = user.UserName ?? "No email provided",
                    Role = role ?? "No role assigned"
                });
            }

            return userDtos;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await _roleManager
                .Roles
                .ToListAsync();

            return roles;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
