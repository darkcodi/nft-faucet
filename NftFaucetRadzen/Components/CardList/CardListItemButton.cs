using Radzen;

namespace NftFaucetRadzen.Components.CardList;

public class CardListItemButton
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public Action Action { get; set; }
    public ButtonStyle Style { get; set; }
}
