using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RenewXControl.Infrastructure.Services;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Hubs;
using RenewXControl.Application.User;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Implementation;
using RenewXControl.Infrastructure.Services.User;
using RenewXControl.Infrastructure.Services.Asset;
using RenewXControl.Application;
using RenewXControl.Domain;

var builder = WebApplication.CreateBuilder(args);

// 👇 Add this CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5187")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IAssetControlFactory, AssetControlFactory>();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddSingleton<IMonitoringRegistry, MonitoringRegistry>();
builder.Services.AddHostedService<MonitoringService>();

// ✅ 5. Others
builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -_.";
    })
    .AddEntityFrameworkStores<RxcDbContext>()
    .AddDefaultTokenProviders();

// jwt
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

            ValidIssuer = "RxcService",
            ValidAudience = "Users",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("5aS*Qm#_^P+zm\\\"c<+g2wk>iOfEm38{BHmG!QG)")
            )
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

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseCors("AllowBlazorClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<AssetsHub>("/assetsHub").RequireAuthorization();
app.MapControllers();
app.Run();

