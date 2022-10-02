using Newtonsoft.Json;

namespace NftFaucet.Domain.Models;

public class TokenMetadata
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("animation_url")]
    public string AnimationUrl { get; set; }

    [JsonProperty("external_url")]
    public string ExternalUrl { get; set; }
}
