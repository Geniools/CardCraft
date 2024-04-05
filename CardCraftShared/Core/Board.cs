using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Board
{
    public ObservableCollection<IMinion> FriendlySide { get; set; }
    public ObservableCollection<IMinion> EnemySide { get; set; }

    
    public Board()
    {
        FriendlySide = new();
        EnemySide = new();
    }

    public void PlayMinionFriendlySide(IMinion card)
    {
        // Check if the board is full
        if (this.FriendlySide.Count >= 7)
        {
            throw new Exception("Board is full");
        }

        this.FriendlySide.Add(card);

        // Subscribe to the minion's death event
        card.OnDeath += (sender, args) =>
        {
            IMinion minion = (IMinion)sender;
            this.RemoveMinionFriendlySide(minion);
        };
    }

    public void PlayMinionEnemySide(IMinion card)
    {
        this.EnemySide.Add(card);

        // Subscribe to the minion's death event
        card.OnDeath += (sender, args) =>
        {
            IMinion minion = (IMinion)sender;
            this.RemoveMinionEnemySide(minion);
        };
    }

    public void RemoveMinionFriendlySide(IMinion card)
    {
        this.FriendlySide.Remove(card);
    }

    public void RemoveMinionEnemySide(IMinion card)
    {
        this.EnemySide.Remove(card);
    }

    internal void EnableMinionsToAttack()
    {
        foreach (IMinion minion in FriendlySide)
        {
            minion.CanAttack = true;
        }
    }
}
