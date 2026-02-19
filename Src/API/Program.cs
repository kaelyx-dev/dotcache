using DotCache.Infrastructure;
using DotCache.API.Middleware;
using DotCache.Infrastructure.Configuration;

ConfigManager configManager = new();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls(configManager.GetServerUrl());

builder.Services.AddSingleton<ConfigManager>(configManager);

WebApplication app = builder.Build();

if (configManager.Get(ConfigTable.Dotcache, "expose_control_api", false))
{
    string controlPath = configManager.Get(ConfigTable.Dotcache, "control_api_path", "__cache")!;
    app.Map(controlPath, _app =>
    {

    });
}

MiddlewareManager.RegisterMiddleware(app);

app.MapGet("/health", () => "OK");

app.Run();
