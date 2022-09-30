namespace NftFaucet.Plugins;

public class Token : IToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ITokenMedia MainFile { get; set; }
    public ITokenMedia CoverFile { get; set; }
}
