using System.Collections.ObjectModel;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Board : ICardStatsManager
{
    public ObservableCollection<IBaseCard> FriendlySide { get; set; }
    public ObservableCollection<IBaseCard> EnemySide { get; set; }

    
    public Board()
    {
        FriendlySide = new();
        EnemySide = new();
    }

    public void PlayMinionFriendlySide(IBaseCard card)
    {
        this.FriendlySide.Add(card);
    }

    public void PlayMinionEnemySide(IBaseCard card)
    {
        this.EnemySide.Add(card);
    }

    public void KillMinion(IBaseCard minion)
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

    public void DamageMinion(IBaseCard minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(IBaseCard minion, int heal)
    {
        throw new NotImplementedException();
    }
}
