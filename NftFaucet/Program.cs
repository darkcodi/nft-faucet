using MetaMask.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NftFaucet;
using NftFaucet.Models;
using NftFaucet.Models.Enums;
using NftFaucet.Options;
using NftFaucet.Services;
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
builder.Services.AddScoped<ExtendedMetamaskService>();
builder.Services.AddScoped<NavigationWrapper>();
builder.Services.AddScoped<IEthereumTransactionService, EthereumTransactionService>();
builder.Services.AddScoped<ISolanaTransactionService, SolanaTransactionService>();
builder.Services.AddScoped<IIpfsService, IpfsService>();
builder.Services.AddScoped<IpfsBlockchainContext>();

builder.Services.AddAntDesign();
builder.Services.AddMetaMaskBlazor();

//var ser = new SolanaTransactionService();
//await ser.MintNft(EthereumNetwork.SolanaDevnet, null, null);

await builder.Build().RunAsync();
