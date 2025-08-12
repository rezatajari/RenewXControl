using API.Utility;
using Infrastructure.Hubs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace API.Extensions;

public static class WebApplicationConfiguration
{
    public static WebApplication UseCustomErrorHandling(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler("/error");

        return app;
    }

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RxcDbContext>();
        dbContext.Database.Migrate();
        return app;
    }

    public static WebApplication UseSwaggerSetup(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }

    public static WebApplication UseStaticFilesAndBlazor(this WebApplication app)
    {
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        // If you want to serve from a custom directory (alternative approach)
        var customStaticFilesPath = Path.Combine(app.Environment.ContentRootPath, "profile-images");
        if (!Directory.Exists(customStaticFilesPath))
        {
            Directory.CreateDirectory(customStaticFilesPath);
        }
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(customStaticFilesPath),
            RequestPath = "/profile-images"
        });

        return app;
    }

    public static WebApplication UseCustomMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<GlobalErrorException>();
        app.UseCors("AllowBlazorClient");
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapHub<AssetsHub>("/assetsHub").RequireAuthorization();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        return app;
    }
}