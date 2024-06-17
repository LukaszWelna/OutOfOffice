using Microsoft.AspNetCore.Identity;
using OutOfOffice.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Seeders
{
    public class UserRoleSeeder
    {
        private readonly OutOfOfficeDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRoleSeeder(OutOfOfficeDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                var email = "admin@admin.com";
                var password = "Admin1!";

                if (await _userManager.FindByEmailAsync("admin@admin.com") == null)
                {
                    var admin = new IdentityUser();
                    admin.UserName = email;
                    admin.Email = email;

                    await _userManager.CreateAsync(admin, password);

                    await _userManager.AddToRoleAsync(admin, "Administrator");
                }
            }
        }
    }
}
