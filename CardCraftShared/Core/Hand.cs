using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand : ICardStatsManager
{
    public List<IBaseCard> Cards { get; init; }

    public Hand()
    {
        this.Cards = new();
    }

    public void Add(IBaseCard card)
    {
        this.Cards.Add(card);
    }

    public IBaseCard Remove(IBaseCard card)
    {
        Cards.Remove(card);
        return card;
    }

    public void DamageAllMinions(int damage)
    {
        throw new NotImplementedException();
    }

    public void HealAllMinions(int heal)
    {
        throw new NotImplementedException();
    }

    public void DamageMinion(IBaseCard minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(IBaseCard minion, int heal)
    {
        throw new NotImplementedException();
    }
}
