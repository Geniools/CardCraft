namespace CardCraftShared.Cards.Spells;

public class CodingBugSpell : BaseSpell
{
    public CodingBugSpell(): base (
        1,
        "Bug",
        "That's a bug!",
        CardRarityEnum.COMMON,
        "codingbugspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}