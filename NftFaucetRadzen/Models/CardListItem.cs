namespace NftFaucetRadzen.Models;

public class CardListItem
{
    public string ImageName { get; set; }
    public string Header { get; set; }
    public bool IsDisabled { get; set; }
    public CardListItemProperty[] Properties { get; set; }
    public CardListItemBadge[] Badges { get; set; }
}
