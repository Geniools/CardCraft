namespace CardCraftShared.Cards.Spells;

public class PraySpell : BaseSpell
{
    public PraySpell(): base(
        3,
        "Pray",
        "Lord give me strength",
        CardRarityEnum.COMMON,
        "prayspell.png"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}