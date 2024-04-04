namespace CardCraftShared.Cards.Spells;

public class NiceArgumentSpell : BaseSpell
{
    public NiceArgumentSpell() : base(
        8,
        "Nice Argument...",
        "... However",
        CardRarityEnum.EPIC,
        "goodargumentspell2.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}