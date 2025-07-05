using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IAssetFactory, AssetFactory>();

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
//builder.Services.AddHostedService<MonitoringService>();
builder.Services.AddControllers();

var app = builder.Build();

// 👇 Use the CORS policy before endpoints
app.UseRouting();
app.UseCors("AllowBlazorClient");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<AssetsHub>("/assetsHub").RequireAuthorization();
app.MapControllers();
app.Run();

