namespace API.Extensions;

public static class InfrastructureService
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddCustomCors(configuration);
        services.AddJwt(configuration);
        services.AddSwaggerSetup();

        return services;
    }
}