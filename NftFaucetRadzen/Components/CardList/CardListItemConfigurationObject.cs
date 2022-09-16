namespace NftFaucetRadzen.Components.CardList;

public class CardListItemConfigurationObject
{
    public Guid Id { get; set; }
    public CardListItemConfigurationObjectType Type { get; set; }

    public string Name { get; set; }
    public string Value { get; set; }
    public string Icon { get; set; }
    public string Placeholder { get; set; }

    // for type=Button only
    public Action ClickAction { get; set; }
}
