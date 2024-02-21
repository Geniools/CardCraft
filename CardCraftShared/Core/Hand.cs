using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand : ICardStatsManager
{
    private List<BaseCard> Cards { get; init; }

    public Hand()
    {
        this.Cards = new();
    }

    public void Add(BaseCard card)
    {
        this.Cards.Add(card);
    }

    public void Remove(BaseCard card)
    {
        this.Cards.Remove(card);
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
