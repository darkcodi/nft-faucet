using CSharpFunctionalExtensions;
using NftFaucet.Domain.Models.Enums;
using NftFaucet.Domain.Utils;
using NftFaucet.Plugins.Models;
using Solnet.Rpc;

namespace NftFaucet.NetworkPlugins.Solana.Networks;

public abstract class SolanaNetwork : Network
{
    public override string Currency { get; } = "SOL";
    public override NetworkType Type { get; } = NetworkType.Solana;
    public override NetworkSubtype SubType { get; } = NetworkSubtype.Solana;
    public override bool SupportsAirdrop { get; } = true;

    public override async Task<Result> Airdrop(string address)
    {
        var client = ClientFactory.GetClient(PublicRpcUrl.OriginalString);
        var airdropResult = await client.RequestAirdropAsync(address, 50000000);
        if (!airdropResult.WasSuccessful)
        {
            return Result.Failure("Failed to request airdrop: " + airdropResult.Reason);
        }

        var wasSuccessful = await RepeatAsync(async () =>
        {
            var transaction = await client.GetTransactionAsync(airdropResult.Result);
            return transaction.WasRequestSuccessfullyHandled && transaction.ErrorData == null;
        }, TimeSpan.FromSeconds(1), 100);
        if (!wasSuccessful)
        {
            return Result.Failure("Failed to retrieve airdrop tx");
        }

        wasSuccessful = await RepeatAsync(async () =>
        {
            var balanceRes = await client.GetBalanceAsync(address);
            return balanceRes.Result.Value > 0;
        }, TimeSpan.FromSeconds(1), 5);
        if (!wasSuccessful)
        {
            return Result.Failure("Airdrop failed: balance is still zero");
        }

        return Result.Success();
    }

    private static async Task<bool> RepeatAsync(Func<Task<bool>> work, TimeSpan retryInterval, int maxExecutionCount = 3)
    {
        for (var i = 0; i < maxExecutionCount; ++i)
        {
            var result = await ResultWrapper.Wrap(work);
            if (result.IsSuccess && result.Value)
            {
                return true;
            }

            await Task.Delay(retryInterval);
        }

        return false;
    }
}
