using OutOfOffice.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.FormInputRenderMode = Microsoft.AspNetCore.Mvc.Rendering.FormInputRenderMode.AlwaysUseCurrentCulture;
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Infrastructure project - services configuration by extension method
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

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
