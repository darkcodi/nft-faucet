using MetaMask.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NftFaucet;
using NftFaucet.ApiClients.NftStorage;
using NftFaucet.Models;
using NftFaucet.Options;
using NftFaucet.Services;
using RestEase;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var settings = new Settings();
builder.Configuration.Bind(settings);
builder.Services.AddSingleton(settings);

Log.Logger = new LoggerConfiguration()
    .WriteTo.BrowserConsole()
    .CreateLogger();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<ScopedAppState>();
builder.Services.AddScoped<RefreshMediator>();
builder.Services.AddScoped<MetamaskInfo>();
builder.Services.AddScoped<NavigationWrapper>();
builder.Services.AddScoped<IEthereumTransactionService, EthereumTransactionService>();
builder.Services.AddScoped<IIpfsService, IpfsService>();
builder.Services.AddSingleton(_ => RestClient.For<INftStorageClient>());

builder.Services.AddAntDesign();
builder.Services.AddMetaMaskBlazor();

await builder.Build().RunAsync();
