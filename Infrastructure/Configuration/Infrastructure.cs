using Application.Common.Interfaces;
using Infrastructure.Repositories;



namespace Infrastructure.Configuration
{
    public static class Infrastructure
    {
        public static IServiceCollection
            InfrastructureConfiguration
            (this IServiceCollection service, IConfiguration configuration)
        {

            ConnectionOptions.ConnectionString = configuration.GetConnectionString("BarayKar");

            service.AddDbContext<SqlServerContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("BarayKar"));


            });

            service.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            service.AddScoped<IContext, ContextRepository>();
            service.AddScoped<IDapper, DapperRepository>();
            return service;
        }
    }
}
