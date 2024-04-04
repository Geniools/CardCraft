namespace CardCraftShared.Cards.Spells;

public class LoremIpsumSpell : BaseSpell
{
    public LoremIpsumSpell() : base(
        2,
        "Lorem Ipsum",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
        CardRarityEnum.COMMON,
        "loremipsumspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}