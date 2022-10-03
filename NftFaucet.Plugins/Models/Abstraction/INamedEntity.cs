namespace NftFaucet.Plugins.Models.Abstraction;

public interface INamedEntity
{
    public Guid Id { get; }
    public string Name { get; }
    public string ShortName { get; }
    public string ImageName { get; }
    public bool IsSupported { get; }
}
