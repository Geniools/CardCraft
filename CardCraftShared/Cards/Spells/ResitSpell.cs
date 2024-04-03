namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell() : base(
        7,
        "Resit", 
        "Resit is a spell that destroys a student's sanity.", 
        CardRarityEnum.EPIC,
        "resitspell.jpeg") { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}
