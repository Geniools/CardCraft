namespace CardCraftShared;

public interface ISpecialAbility
{
    void Trigger(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero);
}
