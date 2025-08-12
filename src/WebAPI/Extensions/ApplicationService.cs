using Application.Common;
using Application.Implementations.Asset;
using Application.Implementations.Monitoring;
using Application.Interfaces.Asset;
using Application.Interfaces.File;
using Application.Interfaces.Monitoring;
using Application.Interfaces.User;
using Infrastructure.Services.Asset;
using Infrastructure.Services.Site;
using Infrastructure.Services.User;
using Infrastructure.Persistence;

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
        services.AddScoped<IAssetControlFactory, AssetControlFactory>();
        services.AddScoped<IMonitoringService, Application.Implementations.Monitoring.MonitoringService>();
        services.AddSingleton<ConnectedUsersStore>();
        services.AddHostedService<Infrastructure.Services.Asset.MonitoringScreen>();

        return services;
    }
}