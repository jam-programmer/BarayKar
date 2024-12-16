using Application.Common;
using Application.Factories.Business;
using Application.Factories.Employment;
using Application.Factories.Home;
using Application.Factories.User;
using Application.Services.Role;
using Application.Services.SMS;
using Application.Services.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class Application
    {
        public static IServiceCollection ApplicationConfiguration
            (this IServiceCollection service, IConfiguration configuration)
        {
            service.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(typeof(FrameworkReference).Assembly));


            service.AddDistributedMemoryCache();
            service.AddScoped<IRole, Role>();
            service.AddScoped<IUser, User>();

            service.AddScoped<IHomeFactory,HomeFactory>();
            service.AddScoped<IUserFactory, UserFactory>();
            service.AddScoped<IBusinessFactory, BusinessFactory>();
            service.AddScoped<IEmploymentFactory, EmploymentFactory>();
            service.AddScoped<ISMSRepository, SMSRepository>();
            return service;
        }
    }
}
