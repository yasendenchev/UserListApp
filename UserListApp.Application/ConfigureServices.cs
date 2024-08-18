using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UserListApp.Application.Services;

namespace UserListApp.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(Assembly.Load("UserListApp.Application"));

        return services;
    }
}
