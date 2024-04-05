using System.Collections.ObjectModel;
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
    }

    public void PlayMinionEnemySide(IMinion card)
    {
        this.EnemySide.Add(card);
    }

    public void RemoveMinionFriendlySide(IMinion card)
    {
        this.FriendlySide.Remove(card);
    }

    public void RemoveMinionEnemySide(IMinion card)
    {
        this.EnemySide.Remove(card);
    }
}
