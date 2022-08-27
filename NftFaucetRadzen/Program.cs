using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NftFaucetRadzen;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Options;
using NftFaucetRadzen.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddSingleton<PluginLoader>();
builder.Services.AddScoped<ScopedAppState>();
builder.Services.AddScoped<RefreshMediator>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
