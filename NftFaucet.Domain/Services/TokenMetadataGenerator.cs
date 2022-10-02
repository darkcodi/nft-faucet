using Newtonsoft.Json;
using NftFaucet.Domain.Models;
using NftFaucet.Domain.Models.Abstraction;

namespace NftFaucet.Domain.Services;

public class TokenMetadataGenerator : ITokenMetadataGenerator
{
    public string GenerateTokenMetadata(IToken token, Uri mainFileLocation, Uri coverFileLocation)
    {
        var tokenMetadata = new TokenMetadata
        {
            Name = token.Name,
            Description = token.Description,
            Image = coverFileLocation != null ? coverFileLocation.OriginalString : mainFileLocation.OriginalString,
            AnimationUrl = coverFileLocation != null ? mainFileLocation.OriginalString : null,
            ExternalUrl = "https://darkcodi.github.io/nft-faucet/",
        };
        var metadataJson = JsonConvert.SerializeObject(tokenMetadata, Formatting.Indented);
        return metadataJson;
    }
}
