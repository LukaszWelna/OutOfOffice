using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Domain.Interfaces;
using OutOfOffice.Infrastructure.Persistence;
using OutOfOffice.Infrastructure.Repositories;
using OutOfOffice.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OutOfOfficeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OutOfOffice")));

            // Identity configuration
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<OutOfOfficeDbContext>();

            // Seeders
            services.AddScoped<RolesSeeder>();
            services.AddScoped<UserRoleSeeder>();

            // Add repositories
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        }
    }
}
