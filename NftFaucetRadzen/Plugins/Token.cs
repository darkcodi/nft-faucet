namespace NftFaucetRadzen.Plugins;

public class Token : IToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ITokenMedia Image { get; set; }
}
