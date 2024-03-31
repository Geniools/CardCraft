using CardCraftShared;
namespace CardCraftShared.Cards.Minions;

public class AlexCard : BaseMinion
{
    public AlexCard() : base(8, 8, 8, "Alex", "Alex is a strong minion", CardRarityEnum.LEGENDARY, "alexcard.jpg")
    {
    }

    public void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}

