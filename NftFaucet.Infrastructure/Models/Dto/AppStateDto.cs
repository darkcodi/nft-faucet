namespace NftFaucet.Infrastructure.Models.Dto;

public class AppStateDto
{
    public Guid Id { get; set; } = Guid.Parse("621a252e-1d8d-4225-a045-b470469730cb");
    public Guid? SelectedNetwork { get; set; }
    public Guid? SelectedProvider { get; set; }
    public Guid? SelectedContract { get; set; }
    public Guid? SelectedToken { get; set; }
    public Guid? SelectedUploadLocation { get; set; }
    public string DestinationAddress { get; set; }
    public int? TokenAmount { get; set; }
}
