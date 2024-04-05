using System.Collections.ObjectModel;

namespace CardCraftShared;

public class Board
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
        // Check if the board is full
        if (this.FriendlySide.Count >= 7)
        {
            throw new Exception("Board is full");
        }
        this.FriendlySide.Add(card);
    }

    public void PlayMinionEnemySide(IBaseCard card)
    {
        this.EnemySide.Add(card);
    }

    public void RemoveMinionFriendlySide(IBaseCard card)
    {
        this.FriendlySide.Remove(card);
    }

    public void RemoveMinionEnemySide(IBaseCard card)
    {
        this.EnemySide.Remove(card);
    }
}
