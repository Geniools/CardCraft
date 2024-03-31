namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell() : base(2, "Resit", "Resit is a spell that destroys a student's sanity.", CardRarityEnum.COMMON,"resitspell.jpeg")
    {
    }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}
