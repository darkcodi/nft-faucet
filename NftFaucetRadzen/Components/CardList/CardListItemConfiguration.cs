namespace NftFaucetRadzen.Components.CardList;

public class CardListItemConfiguration
{
    public CardListItemConfigurationObject[] Objects { get; set; }
    public Func<CardListItemConfigurationObject[], Task<bool>> ValidationFunc { get; set; }
    public Func<CardListItemConfigurationObject[], Task> ConfigureAction { get; set; }
}
