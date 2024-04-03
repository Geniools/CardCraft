namespace CardCraftShared.Cards.Spells;

public class SixOneProjectResitSpell : BaseSpell
{
    public SixOneProjectResitSpell() : base(
        4,
        "Project6.1 Resit",
        "Dear students, We regret to inform you that the resit did not result in a pass for project 6.1",
        CardRarityEnum.COMMON,
        "sixoneprojectresitspell.png"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}