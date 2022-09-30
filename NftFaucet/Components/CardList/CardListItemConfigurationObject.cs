namespace NftFaucet.Components.CardList;

public class CardListItemConfigurationObject
{
    public CardListItemConfigurationObjectType Type { get; set; }

    public string Name { get; set; }
    public string Value { get; set; }
    public string Icon { get; set; }
    public string Placeholder { get; set; }
    public bool IsDisabled { get; set; }

    // for type=Button only
    public Action ClickAction { get; set; }
}
