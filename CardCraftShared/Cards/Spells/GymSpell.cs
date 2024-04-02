namespace CardCraftShared.Cards.Spells;

public class GymSpell : BaseSpell
{
    public GymSpell() : base(
        3,
        "Gym",
        "E",
        CardRarityEnum.RARE,
        "gymspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}