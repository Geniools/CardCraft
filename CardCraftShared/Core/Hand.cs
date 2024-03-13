using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand : ICardStatsManager
{
    private List<IMinion> Cards { get; init; }

    public Hand()
    {
        this.Cards = [];
    }

    public void Add(IMinion card)
    {
        this.Cards.Add(card);
    }

    public void Remove(IMinion card)
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

    public void DamageMinion(IMinion minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(IMinion minion, int heal)
    {
        throw new NotImplementedException();
    }
}
