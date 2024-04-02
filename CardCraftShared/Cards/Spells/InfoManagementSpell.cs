namespace CardCraftShared.Cards.Spells;

public class InfoManagementSpell : BaseSpell
{
    public InfoManagementSpell() : base(
        7,
        "InfoManagement",
        "3ECs",
        CardRarityEnum.COMMON,
        "infomanagementspell.jpeg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}