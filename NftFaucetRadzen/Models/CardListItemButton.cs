using Radzen;

namespace NftFaucetRadzen.Models;

public class CardListItemButton
{
    public string Name { get; set; }
    public Action Action { get; set; }
    public ButtonStyle Style { get; set; }
}
