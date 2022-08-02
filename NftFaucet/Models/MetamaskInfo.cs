using System.Globalization;
using System.Numerics;
using Ethereum.MetaMask.Blazor;
using Nethereum.Hex.HexTypes;
using NftFaucet.Models.Enums;
using NftFaucet.Services;
using NftFaucet.Utils;
using Serilog;

namespace NftFaucet.Models;

public class MetamaskInfo
{
    private readonly RefreshMediator _refreshMediator;

    public MetamaskInfo(IMetaMaskService service, MetamaskSigningService signingService, RefreshMediator refreshMediator)
    {
        Service = service;
        SigningService = signingService;
        _refreshMediator = refreshMediator;
    }

    public IMetaMaskService Service { get; }
    public MetamaskSigningService SigningService { get; }

    public bool? HasMetaMask { get; private set; }
    public bool? IsMetaMaskConnected { get; private set; }

    public string Address { get; private set; }
    public BigInteger ChainId { get; private set; }
    public NetworkChain? Network { get; private set; }

    public async Task<bool> IsConnected()
    {
        HasMetaMask ??= await Service.IsMetaMaskAvailableAsync();
        IsMetaMaskConnected ??= await Service.IsSiteConnectedAsync();

        return HasMetaMask.Value && IsMetaMaskConnected.Value;
    }

    public async Task<bool> IsReady()
    {
        if (!await IsConnected())
            return false;

        if (string.IsNullOrEmpty(Address) || ChainId == 0)
        {
            await RefreshAddress();
            SubscribeToEvents();
        }

        return HasMetaMask!.Value && IsMetaMaskConnected!.Value && !string.IsNullOrEmpty(Address) && ChainId != 0;
    }

    public async Task<bool> Connect()
    {
        var result = await ResultWrapper.Wrap(() => Service.ConnectAsync());
        if (result.IsFailure)
        {
            Log.Error(result.Error);
            return false;
        }

        HasMetaMask = true;
        IsMetaMaskConnected = true;
        await RefreshAddress();
        SubscribeToEvents();

        return true;
    }

    public async Task RefreshAddress()
    {
        Address = await Service.GetSelectedAccountAsync();
        var chainIdHex = await Service.GetSelectedChainAsync();
        ChainId = !string.IsNullOrEmpty(chainIdHex) ? new HexBigInteger(chainIdHex).Value : BigInteger.Zero;
        Network = long.TryParse(ChainId.ToString(), out var longChainId) && Enum.IsDefined(typeof(NetworkChain), longChainId) ? (NetworkChain) longChainId : null;
        _refreshMediator.NotifyStateHasChangedSafe();
    }

    private void SubscribeToEvents()
    {
        Service.AccountsChanged += OnAccountChangedEvent;
        Service.ChainChanged += OnChainChangedEvent;
    }

    private void OnAccountChangedEvent(object sender, string[] args)
    {
        Address = args.FirstOrDefault();
        _refreshMediator.NotifyStateHasChangedSafe();
    }

    private void OnChainChangedEvent(object sender, string chainIdHex)
    {
        ChainId = !string.IsNullOrEmpty(chainIdHex) ? new HexBigInteger(chainIdHex).Value : BigInteger.Zero;
        Network = long.TryParse(ChainId.ToString(), out var longChainId) && Enum.IsDefined(typeof(NetworkChain), longChainId) ? (NetworkChain) longChainId : null;
        _refreshMediator.NotifyStateHasChangedSafe();
    }
}
