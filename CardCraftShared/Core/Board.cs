using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Board : ICardStatsManager
{
    private List<IBaseCard> Cards { get; init; }

    public Board()
    {
        Cards = [];
    }

    public void PlayMinion(BaseMinion minion)
    {
        throw new NotImplementedException();
    }

    public void KillMinion(BaseMinion minion)
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

    public void DamageMinion(IMinion minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(IMinion minion, int heal)
    {
        throw new NotImplementedException();
    }
}
