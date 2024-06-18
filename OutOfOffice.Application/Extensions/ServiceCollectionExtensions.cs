using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.Mappings;
using OutOfOffice.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OutOfOffice.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            // Add Auto mapper
            services.AddAutoMapper(typeof(UserRoleMappingProfile));

            // Add services
            services.AddScoped<IUserContextService, UserContextService>();

            services.AddScoped<IUserRoleService, UserRoleService>();
        }
    }
}
