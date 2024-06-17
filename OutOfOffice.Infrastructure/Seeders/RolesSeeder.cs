using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Seeders
{
    public class RolesSeeder
    {
        private readonly OutOfOfficeDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesSeeder(OutOfOfficeDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                var roles = new[] { "Employee", "HR Manager", "Project Manager", "Administrator" };

                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }
    }
}
