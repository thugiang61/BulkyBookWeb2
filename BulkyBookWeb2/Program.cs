using BulkyBookWeb2.Data;
using BulkyBookWeb2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BulkyBookWeb2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BulkyBookWeb2Context")));

// Add services to the container.
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/GoogleLogin";
    })
    .AddGoogle(options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        }
        else if (builder.Environment.IsProduction())
        {
           #pragma warning disable CS8601 // Possible null reference assignment.
            options.ClientId = Environment.GetEnvironmentVariable("ClientId");
            options.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret");
           #pragma warning restore CS8601 // Possible null reference assignment.
        }

        options.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            context.Response.Redirect(context.RedirectUri + "&prompt=consent");
            return Task.CompletedTask;
        };
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    if (app.Environment.IsDevelopment())
        SeedData.Initialize(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
