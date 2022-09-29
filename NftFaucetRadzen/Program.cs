using Ethereum.MetaMask.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NftFaucetRadzen;
using NftFaucetRadzen.Models;
using NftFaucetRadzen.Models.State;
using NftFaucetRadzen.Options;
using NftFaucetRadzen.Services;
using Radzen;
using TG.Blazor.IndexedDB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddSingleton<PluginLoader>();
builder.Services.AddSingleton<Mapper>();
builder.Services.AddScoped<StateRepository>();
builder.Services.AddScoped<ScopedAppState>();
builder.Services.AddScoped<RefreshMediator>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddMetaMaskBlazor();

builder.Services.AddIndexedDB(dbStore =>
{
    dbStore.DbName = "NftFaucet";
    dbStore.Version = 1;

    dbStore.Stores.Add(new StoreSchema
    {
        Name = "AppState",
        PrimaryKey = new IndexSpec { Name = "id", KeyPath = "id", Auto = true },
        Indexes = new List<IndexSpec>
        {
            new IndexSpec {Name = "selectedNetwork", KeyPath = "selectedNetwork", Auto = false},
            new IndexSpec {Name = "selectedProvider", KeyPath = "selectedProvider", Auto = false},
            new IndexSpec {Name = "selectedContract", KeyPath = "selectedContract", Auto = false},
            new IndexSpec {Name = "selectedToken", KeyPath = "selectedToken", Auto = false},
            new IndexSpec {Name = "selectedUploadLocation", KeyPath = "selectedUploadLocation", Auto = false},
            new IndexSpec {Name = "destinationAddress", KeyPath = "destinationAddress", Auto = false},
            new IndexSpec {Name = "tokenAmount", KeyPath = "tokenAmount", Auto = false},
        }
    });
});

await builder.Build().RunAsync();
