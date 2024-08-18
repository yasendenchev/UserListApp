using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserListApp.Infrastructure.Persistance;
using UserListApp.Infrastructure.Repositories;

namespace UserListApp.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer<UserListAppContext>(configuration.GetConnectionString("DefaultConnection"));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
