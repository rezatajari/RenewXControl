using Application.Common;
using Application.Implementations.Asset;
using Application.Interfaces.Asset;
using Application.Interfaces.User;
using Infrastructure.Services.User;
using Infrastructure.Persistence;
using Application.Interfaces;
using Infrastructure.Services;
using Application.DTOs;
using Infrastructure;

namespace API.Extensions;

public static class ApplicationService
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        // Register application layer services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddSingleton<List<UserMonitoringInfo>>();
        services.AddScoped<IMonitoringService, Application.Implementations.Monitoring.MonitoringService>();
        services.AddHostedService<MonitoringScreen>();

        return services;
    }
}