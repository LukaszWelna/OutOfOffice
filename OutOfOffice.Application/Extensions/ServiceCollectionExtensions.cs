using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OutOfOffice.Application.ApplicationUser;
using OutOfOffice.Application.Mappings;
using OutOfOffice.Application.Services;
using OutOfOffice.Application.Validators;

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
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();

            // Add Fluent validation
            services.AddValidatorsFromAssemblyContaining<UserRoleDtoValidator>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();
        }
    }
}
