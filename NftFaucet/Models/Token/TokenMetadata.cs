using Newtonsoft.Json;

namespace NftFaucet.Models.Token;

public class TokenMetadata
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("external_url")]
    public string ExternalUrl { get; set; }
}
