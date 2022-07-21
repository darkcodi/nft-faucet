using MetaMask.Blazor;
using MetaMask.Blazor.Enums;
using NftFaucet.Models.Enums;
using NftFaucet.Services;
using NftFaucet.Utils;
using Serilog;

namespace NftFaucet.Models;

public class MetamaskInfo
{
    private readonly RefreshMediator _refreshMediator;

    public MetamaskInfo(ExtendedMetamaskService service, RefreshMediator refreshMediator)
    {
        Service = service;
        _refreshMediator = refreshMediator;
    }

    public ExtendedMetamaskService Service { get; }

    public bool? HasMetaMask { get; private set; }
    public bool? IsMetaMaskConnected { get; private set; }

    public string Address { get; private set; }
    public long ChainId { get; private set; }
    public NetworkChain? Network { get; private set; }

    public async Task<bool> IsConnected()
    {
        HasMetaMask ??= await Service.HasMetaMask();
        IsMetaMaskConnected ??= await Service.IsSiteConnected();

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
        var result = await ResultWrapper.Wrap(() => Service.ConnectMetaMask());
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
        Address = await Service.GetSelectedAddress();
        ChainId = (await Service.GetSelectedChain()).chainId;
        Network = Enum.IsDefined(typeof(NetworkChain), ChainId) ? (NetworkChain) ChainId : null;
        _refreshMediator.NotifyStateHasChangedSafe();
    }

    private void SubscribeToEvents()
    {
        MetaMaskService.AccountChangedEvent += OnAccountChangedEvent;
        MetaMaskService.ChainChangedEvent += OnChainChangedEvent;
        Service.ListenToEvents();
    }

    private Task OnAccountChangedEvent(string newAddress)
    {
        Address = newAddress;
        _refreshMediator.NotifyStateHasChangedSafe();
        return Task.CompletedTask;
    }

    private Task OnChainChangedEvent((long ChainId, Chain Chain) arg)
    {
        ChainId = arg.ChainId;
        Network = Enum.IsDefined(typeof(NetworkChain), ChainId) ? (NetworkChain) ChainId : null;
        _refreshMediator.NotifyStateHasChangedSafe();
        return Task.CompletedTask;
    }
}
