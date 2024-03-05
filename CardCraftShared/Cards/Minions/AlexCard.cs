using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion
{
    public AlexCard(int manaCost, string name, string description, CardRarityEnum rarity, int health, int attack) : base(manaCost, name, description, rarity, health, attack)
    {

    }

    public void Trigger(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}

