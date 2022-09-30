namespace NftFaucet.Plugins.NetworkPlugins;

public class Contract : IContract
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Address { get; set; }
    public ContractType Type { get; set; }
    public string DeploymentTxHash { get; set; }
    public DateTime? DeployedAt { get; set; }
    public bool IsVerified { get; set; }
}
