using API.Utility;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class PresentationService
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddAuthorization();
        services.AddSignalR();
     
        return services;
    }
}