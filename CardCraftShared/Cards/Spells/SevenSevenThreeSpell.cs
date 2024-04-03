namespace CardCraftShared.Cards.Spells;

public class SevenSevenThreeSpell : BaseSpell
{
    public SevenSevenThreeSpell() : base(
        10,
        "773",
        "Secret spell that deals 6 damage to the enemy hero",
        CardRarityEnum.LEGENDARY,
        "sevenseventhreespell.png"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}