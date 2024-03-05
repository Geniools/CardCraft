namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell(int manaCost, string name, string description, CardRarityEnum rarity) : base(manaCost, name, description, rarity)
    {

    }

    public override void Trigger(Deck deck, Deck enemyDeck, Board board, Board enemyBoard, BaseHero hero, BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}
