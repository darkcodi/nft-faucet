using CSharpFunctionalExtensions;

namespace NftFaucet.Components.CardList;

public class CardListItemConfiguration
{
    public CardListItemConfigurationObject[] Objects { get; set; }
    public Func<CardListItemConfigurationObject[], Task<Result>> ConfigureAction { get; set; }
}
