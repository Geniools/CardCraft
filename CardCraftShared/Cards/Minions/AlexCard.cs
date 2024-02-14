using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion, ISpecialAbility
{
    public AlexCard(int manaCost, string name, string description, CardRarity rarity) : base(manaCost, name, description, rarity)
    {
    }

    void ISpecialAbility.TriggerSpecialAbility(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}

