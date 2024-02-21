namespace CardCraftShared;

public class Graveyard
{
    private List<BaseCard> Cards { get; set; } = new();

    public void AddCard(BaseCard card)
    {
        Cards.Add(card);
    }

    public void RemoveCard(BaseCard card)
    {
        Cards.Remove(card);
    }
}
