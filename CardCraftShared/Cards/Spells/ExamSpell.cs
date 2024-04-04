namespace CardCraftShared.Cards.Spells;

public class ExamSpell : BaseSpell
{
    public ExamSpell() : base(
        3,
        "Exam",
        "Ay blayt",
        CardRarityEnum.COMMON,
        "examspell.jpeg"
    )
    { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}