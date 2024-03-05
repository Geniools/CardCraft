namespace CardCraftShared;

public abstract class BaseSpell : BaseCard
{

    protected BaseSpell(int manaCost, string name, string description, CardRarityEnum rarity)
        : base(manaCost, name, description, rarity)
    {

    }
    public abstract void Trigger(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero);
}
