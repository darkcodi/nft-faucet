using NftFaucet.Domain.Models.Abstraction;

namespace NftFaucet.Infrastructure.Models.State;

public class UserStateStorage
{
    public ICollection<IToken> Tokens { get; set; }
    public ICollection<ITokenUploadLocation> UploadLocations { get; set; }

    public Guid[] SelectedNetworks { get; set; }
    public Guid[] SelectedWallets { get; set; }
    public Guid[] SelectedContracts { get; set; }
    public Guid[] SelectedTokens { get; set; }
    public Guid[] SelectedUploadLocations { get; set; }
    public string DestinationAddress { get; set; }
    public int TokenAmount { get; set; } = 1;
}
