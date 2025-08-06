using Application.Common;
using Application.Implementations.Asset;
using Application.Implementations.Monitoring;
using Application.Implementations;
using Application.Interfaces.Asset;
using Application.Interfaces.File;
using Application.Interfaces.Monitoring;
using Application.Interfaces.User;
using Infrastructure.Services.Asset;
using Infrastructure.Services.Site;
using Infrastructure.Services;
using Infrastructure;
using Infrastructure.Services.User;

namespace API.Extensions;

public static class ApplicationService
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        // Register application layer services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<ISiteRepository, SiteRepository>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IAssetControlFactory, AssetControlFactory>();
        services.AddSingleton<IMonitoringRegistry, MonitoringRegistry>();
        services.AddScoped<IMonitoringService, MonitoringService>();
        services.AddHostedService<MonitoringScreen>();

        return services;
    }