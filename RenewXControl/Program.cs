using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Hubs;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Implementation;
using RenewXControl.Infrastructure.Services.User;
using RenewXControl.Infrastructure.Services.Asset;
using RenewXControl.Api;
using RenewXControl.Api.Utility;
using RenewXControl.Application.Asset.Interfaces.Asset;
using RenewXControl.Application.Asset.Interfaces.Monitoring;
using RenewXControl.Application.Asset.Implementation.Monitoring;
using RenewXControl.Application.Asset.Implementation.Asset;
using RenewXControl.Application.User.Interfaces;
using RenewXControl.Application.User.Implementations;
using RenewXControl.Domain.User;
using System.Reflection;
using RenewXControl.Application;
using RenewXControl.Infrastructure.Services.Site;
using Microsoft.OpenApi.Models;
using RenewXControl.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// 👇 Add this CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        var allowedOrigins = builder.Configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>();

        policy.WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "RenewXControl API", Version = "v1" });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<ISiteService, SiteService>();
builder.Services.AddScoped<ISiteRepository,SiteRepository>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetControlFactory, AssetControlFactory>();
builder.Services.AddSingleton<IMonitoringRegistry, MonitoringRegistry>();
builder.Services.AddScoped<IMonitoringService, MonitoringService>();
builder.Services.AddHostedService<MonitoringScreen>();

// ✅ 5. Others
builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(name: "DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -_.";
    })
    .AddEntityFrameworkStores<RxcDbContext>()
    .AddDefaultTokenProviders();

// jwt
var jwtSection = builder.Configuration.GetSection(key: "JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);
var jwtSettings = jwtSection.Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

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
            RoleClaimType = ClaimTypes.Role,

            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // Support SignalR with query token
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

builder.Services.AddSignalR();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});

var app = builder.Build();

//var scope = app.Services.CreateScope();
//var dbContext = scope.ServiceProvider.GetService<RxcDbContext>();
//var pendingMigration = dbContext.Database.GetPendingMigrations();
//if (pendingMigration.Any())
//{
//    dbContext.Database.Migrate();
//}

app.UseSwagger();
app.UseSwaggerUI();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<GlobalErrorException>();
app.UseCors("AllowBlazorClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<AssetsHub>(pattern:"/assetsHub").RequireAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();

