using Microsoft.OpenApi;
using System.Reflection;

namespace API.Extensions;

public static class Swagger
{
    public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}