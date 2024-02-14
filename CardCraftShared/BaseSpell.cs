namespace CardCraftShared;

public abstract class BaseSpell : BaseCard
{
    protected BaseSpell(int manaCost, string name, string description, CardRarity rarity) : base(manaCost, name, description, rarity)
    {
    }
}
