using Application.Common;
using Application.Configuration;
using Domain.Entities.System.Identity;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Contexts;
using Microsoft.AspNetCore.Identity;
using NLog;
using NLog.Web;
var logger = NLog.LogManager
    .Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

builder.Host.UseNLog();






builder.Services.ApplicationConfiguration(builder.Configuration);
builder.Services.InfrastructureConfiguration(builder.Configuration);

builder.Services.AddIdentity<UserEntity, RoleEntity>().AddEntityFrameworkStores<SqlServerContext>()
    .AddRoles<RoleEntity>()
    .AddDefaultTokenProviders().AddErrorDescriber<PersianIdentityError>(); ;
//Identity
builder.Services.Configure<IdentityOptions>(option =>
{
    option.User.RequireUniqueEmail = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequiredLength = 6;
    option.SignIn.RequireConfirmedEmail = true;
    option.SignIn.RequireConfirmedAccount = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;
    option.Lockout.MaxFailedAccessAttempts = 4;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    option.User.RequireUniqueEmail = true;
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequiredUniqueChars = 1;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
});
builder.Services.ConfigureApplicationCookie(cooke =>
{
    cooke.ExpireTimeSpan = TimeSpan.FromDays(30);
    cooke.LoginPath = "/Identity/SignIn";
    cooke.AccessDeniedPath = "/";
    cooke.SlidingExpiration = true;
});
//Identity



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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
