using NftFaucet.Domain.Models.Abstraction;

namespace NftFaucet.Domain.Services;

public interface ITokenMetadataGenerator
{
    public string GenerateTokenMetadata(IToken token, Uri mainFileLocation, Uri coverFileLocation);
}
