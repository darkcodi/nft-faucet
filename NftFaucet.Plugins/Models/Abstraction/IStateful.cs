namespace NftFaucet.Plugins.Models.Abstraction;

public interface IStateful
{
    public Task<string> GetState();
    public Task SetState(string state);
}
