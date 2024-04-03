namespace CardCraftShared.Cards.Spells;

public class IotLabSpell : BaseSpell
{
    public IotLabSpell() : base(
        3,
        "IoT Lab",
        "Increased productivity, 2 extra mana to the hero",
        CardRarityEnum.COMMON,
        "iotlabspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}