using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Deck : ICardStatsManager
{
    public List<BaseCard> Cards { get; init; }

    public Deck()
    {
        this.Cards = new();
    }

    public void Shuffle()
    {
        throw new NotImplementedException();
    }

    public BaseCard DrawCard()
    {
        throw new NotImplementedException();
    }

    public void DamageAllMinions(int damage)
    {
        throw new NotImplementedException();
    }

    public void HealAllMinions(int heal)
    {
        throw new NotImplementedException();
    }

    public void DamageMinion(BaseMinion minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(BaseMinion minion, int heal)
    {
        throw new NotImplementedException();
    }
}
