namespace API.Extensions;

public static class Cors
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(

            options =>
            {
                options.AddPolicy("AllowBlazorClient", policy =>
                {
                    var allowedOrigins = configuration
                        .GetSection("Cors:AllowedOrigins")
                        .Get<string[]>();

                    policy.WithOrigins(allowedOrigins!)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            }); 

        return services;
    }
}