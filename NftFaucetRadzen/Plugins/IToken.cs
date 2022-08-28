namespace NftFaucetRadzen.Plugins;

public interface IToken
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; }
    public ITokenMedia Image { get; }
}
