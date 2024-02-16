namespace CardCraftShared;

public abstract class BaseSpell : BaseCard, ISpecialAbility
{
    protected BaseSpell(int manaCost, string name, string description, CardRarity rarity) : base(manaCost, name, description, rarity)
    {

    }

    public void TriggerSpecialAbility(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}
