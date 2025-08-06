using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;
builder.Configuration.AddAppConfigurations(builder.Environment);

builder.Services
    .RegisterInfrastructure(builder.Configuration)
    .RegisterApplication()
    .RegisterPresentation();


var app = builder.Build();

app.UseCustomErrorHandling();
app.ApplyMigrations();
app.UseSwaggerSetup();

app.UseStaticFilesAndBlazor();

app.UseRouting();
app.UseCustomMiddlewares();

app.MapEndpoints();

app.Run();
