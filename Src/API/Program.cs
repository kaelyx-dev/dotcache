using DotCache.Infrastructure;
using DotCache.API.Middleware;
using DotCache.Infrastructure.Configuration;

ConfigManager configManager = new();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(configManager.GetServerUrl());

builder.Services.AddSingleton<ConfigManager>(configManager);

WebApplication app = builder.Build();

MiddlewareManager.RegisterMiddleware(app);

app.MapGet("/health", () => "OK");

app.Run();
