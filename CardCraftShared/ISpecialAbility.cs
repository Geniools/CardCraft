namespace CardCraftShared;

public interface ISpecialAbility
{
    void TriggerSpecialAbility(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero);
}
