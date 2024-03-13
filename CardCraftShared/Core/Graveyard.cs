namespace CardCraftShared;

public class Graveyard
{
    private List<IBaseCard> Cards { get; set; } = [];

    public void AddCard(IBaseCard card)
    {
        Cards.Add(card);
    }

    public void RemoveCard(IBaseCard card)
    {
        Cards.Remove(card);
    }
}
