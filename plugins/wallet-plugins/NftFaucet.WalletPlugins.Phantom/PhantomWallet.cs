using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Plugins.Models;
using NftFaucet.Plugins.Models.Abstraction;

namespace NftFaucet.WalletPlugins.Phantom;

public class PhantomWallet : Wallet
{
    public override Guid Id { get; } = Guid.Parse("ae860901-5441-463c-a16e-4786f041500d");
    public override string Name { get; } = "Phantom";
    public override string ShortName { get; } = "Phantom";
    public override string ImageName { get; } = "phantom.svg";
    public override bool IsSupported { get; } = false;

    public override Property[] GetProperties()
        => new Property[]
        {
            new Property{ Name = "Installed", Value = "YES" },
            new Property{ Name = "Connected", Value = IsConfigured ? "YES" : "NO" },
        };

    public override bool IsNetworkSupported(INetwork network)
        => network?.Type == NetworkType.Solana;

    public override Task<string> GetAddress()
    {
        throw new NotImplementedException();
    }

    public override Task<Balance> GetBalance(INetwork network)
    {
        throw new NotImplementedException();
    }

    public override Task<INetwork> GetNetwork(IReadOnlyCollection<INetwork> allKnownNetworks, INetwork selectedNetwork)
    {
        throw new NotImplementedException();
    }

    public override Task<string> Mint(MintRequest mintRequest)
    {
        throw new NotImplementedException();
    }
}
