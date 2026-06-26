using API;
using API.Extensions;
using Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAppConfigurations(builder.Environment);

builder.Services
    .RegisterInfrastructure(builder.Configuration)
    .RegisterApplication()
    .RegisterPresentation();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddFluentValidationAutoValidation();
var app = builder.Build();

app.UseCustomErrorHandling();
app.UseSwaggerSetup();

app.UseStaticFilesAndBlazor();

app.UseRouting();
app.UseCustomMiddlewares();

app.MapEndpoints();

app.Run();
