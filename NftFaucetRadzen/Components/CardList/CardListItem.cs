namespace NftFaucetRadzen.Components.CardList;

public class CardListItem
{
    public Guid Id { get; set; }
    public string ImageLocation { get; set; }
    public string Header { get; set; }
    public bool IsDisabled { get; set; }
    public CardListItemProperty[] Properties { get; set; } = Array.Empty<CardListItemProperty>();
    public CardListItemBadge[] Badges { get; set; } = Array.Empty<CardListItemBadge>();
    public CardListItemConfiguration Configuration { get; set; }
}
