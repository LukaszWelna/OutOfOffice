﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Domain.Models;

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

        public async Task UpdateUserRoleAsync(UserRoleDto userRoleDto)
        {
            var user = await _userManager.FindByIdAsync(userRoleDto.SelectedUserId);

            if (user == null)
            {
                throw new InvalidOperationException("There is no user with selected ID.");
            }

            var role = await _roleManager.FindByIdAsync(userRoleDto.SelectedRoleId);

            if (role == null)
            {
                throw new InvalidOperationException("There is no role with selected ID.");
            }

            // Delete current user roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Add new role to user
            await _userManager.AddToRoleAsync(user, role.Name ?? "");
        }
    }
}
