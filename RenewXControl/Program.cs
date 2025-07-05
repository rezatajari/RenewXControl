using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using RenewXControl.Configuration.AssetsModel.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Domain.Users;
using RenewXControl.Infrastructure.Services;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Hubs;
using RenewXControl.Application.User;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Implementation;
using RenewXControl.Application.Common;
using RenewXControl.Infrastructure.Services.User;
using RenewXControl.Infrastructure.Services.Asset;

var builder = WebApplication.CreateBuilder(args);

// 👇 Add this CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ✅ 1. load config
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// JWT
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "RxcService",
            ValidAudience = "Users",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("5aS*Qm#_^P+zm\\\"c<+g2wk>iOfEm38{BHmG!QG)"))
        };

        // 👇 This is required for SignalR (to get token from query string)
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // 👇 If request is for the hub path, read from query string
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/assetsHub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// ✅ 2. Bind it
//builder.Services.Configure<SolarPanelConfig>(
//    builder.Configuration.GetSection("SolarPanelConfig"));
//builder.Services.Configure<WindTurbineConfig>(
//    builder.Configuration.GetSection("WindTurbineConfig"));
//builder.Services.Configure<BatteryConfig>(
//    builder.Configuration.GetSection("BatteryConfig"));

// ✅ 3. SolarPanel & Turbine setup
//builder.Services.AddSingleton<SolarPanel>(sp =>
//{
//    var config=sp.GetRequiredService<IOptions<SolarPanelConfig>>().Value;
//    return SolarPanel.Create(config);
//});
//builder.Services.AddSingleton<WindTurbine>(wt =>
//{
//    var config = wt.GetRequiredService<IOptions<WindTurbineConfig>>().Value;
//    return WindTurbine.Create(config);
//});
//builder.Services.AddSingleton<Battery>(b =>
//{
//    var config = b.GetRequiredService<IOptions<BatteryConfig>>().Value;
//    return Battery.Create(config);
//});

// ✅ 4. Register solar & turbine services and interfaces
//builder.Services.AddSingleton<ISolarControl, SolarControl>();
//builder.Services.AddSingleton<ISolarService, SolarService>();
//builder.Services.AddSingleton<ITurbineService, TurbineService>();
//builder.Services.AddSingleton<ITurbineControl, TurbineControl>();
//builder.Services.AddSingleton<IBatteryControl, BatteryControl>();
//builder.Services.AddSingleton<IBatteryService, BatteryService>();
//builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IAssetFactory, AssetFactory>();

// ✅ 5. Others
builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<RxcDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();
//builder.Services.AddHostedService<MonitoringService>();
builder.Services.AddControllers();

var app = builder.Build();

// 👇 Use the CORS policy before endpoints
app.UseCors("AllowBlazorClient");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<AssetsHub>("/assetsHub").RequireAuthorization();
app.MapControllers();
app.Run();

