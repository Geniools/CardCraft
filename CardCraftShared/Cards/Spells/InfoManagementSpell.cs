namespace CardCraftShared.Cards.Spells;

public class InfoManagementSpell : BaseSpell
{
    public InfoManagementSpell() : base(
        6,
        "InfoManagement",
        "3ECs",
        CardRarityEnum.RARE,
        "infomanagementspell.jpeg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}