using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions;

public static class Database
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RxcDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(name: "DefaultConnection")));

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -_.";
                })
                .AddEntityFrameworkStores<RxcDbContext>()
                .AddDefaultTokenProviders();

        return services;
    }
}