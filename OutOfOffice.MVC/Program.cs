using OutOfOffice.Infrastructure.Extensions;
using OutOfOffice.Application.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Infrastructure.Persistence;
using OutOfOffice.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.FormInputRenderMode = Microsoft.AspNetCore.Mvc.Rendering.FormInputRenderMode.AlwaysUseCurrentCulture;
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Application project - services configuration by extension method
builder.Services.AddApplication();

// Infrastructure project - services configuration by extension method
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Add pending migrations
using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<OutOfOfficeDbContext>();

var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

// Seed data
var rolesSeeder = scope.ServiceProvider.GetRequiredService<RolesSeeder>();
await rolesSeeder.Seed();

var userRoleSeeder = scope.ServiceProvider.GetRequiredService<UserRoleSeeder>();
await userRoleSeeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
