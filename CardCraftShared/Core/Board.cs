using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftShared.Core.Interfaces;
using CardCraftShared.Core.Other;

namespace CardCraftShared;

public class Board
{
    public const int MAX_BOARD_SIZE = 6;

    public ObservableCollection<BaseMinion> FriendlySide { get; set; }
    public ObservableCollection<BaseMinion> EnemySide { get; set; }

    public Action<BaseMinion> OnMinionDeath;

    
    public Board()
    {
        FriendlySide = new();
        EnemySide = new();
    }

    public void PlayMinionFriendlySide(BaseMinion card)
    {
        // Check if the board is full
        if (this.FriendlySide.Count > MAX_BOARD_SIZE)
        {
            throw new Exception("Board is full");
        }

        this.FriendlySide.Add(card);

        // Subscribe to the minion's death event
        card.OnDeath += (sender, args) =>
        {
            BaseMinion minion = (BaseMinion)sender;
            this.RemoveMinionFriendlySide(minion);

            this.OnMinionDeath?.Invoke(minion);
        };
    }

    public void PlayMinionEnemySide(BaseMinion card)
    {
        this.EnemySide.Add(card);

        // Subscribe to the minion's death event
        card.OnDeath += (sender, args) =>
        {
            BaseMinion minion = (BaseMinion)sender;
            this.RemoveMinionEnemySide(minion);

            this.OnMinionDeath?.Invoke(minion);
        };
    }

    public void RemoveMinionFriendlySide(BaseMinion card)
    {
        this.FriendlySide.Remove(card);
    }

    public void RemoveMinionEnemySide(BaseMinion card)
    {
        this.EnemySide.Remove(card);
    }

    public void EnableMinionsToAttack()
    {
        foreach (IMinion minion in FriendlySide)
        {
            minion.CanAttack = true;
        }
    }
}
