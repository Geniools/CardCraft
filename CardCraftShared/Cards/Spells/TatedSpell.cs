namespace CardCraftShared.Cards.Spells;

public class TatedSpell : BaseSpell
{
    public TatedSpell() : base
    (
        4,
        "Tated",
        "You have been 'Tated'! Don't leave your laptop open!",
        CardRarityEnum.EPIC,
        "tatedspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}