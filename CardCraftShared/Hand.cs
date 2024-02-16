namespace CardCraftShared;

public class Hand
{
    private readonly List<BaseCard> _cards;

    public Hand()
    {
        this._cards = new List<BaseCard>();
    }

    public void Add(BaseCard card)
    {
        this._cards.Add(card);
    }

    public void Remove(BaseCard card)
    {
        this._cards.Remove(card);
    }
}
