using System.Collections.ObjectModel;
using CardCraftShared.Core.Interfaces;

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
