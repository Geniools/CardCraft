using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Board : ICardStatsManager
{
    public List<IBaseCard> FriendlySide { get; set; }
    public List<IBaseCard> EnemySide { get; set; }

    
    public Board()
    {
        FriendlySide = new();
        EnemySide = new();
    }

    public void AddCard(IBaseCard card, Player player)
    {
        // TODO: Add a card to the correct side of the board
        // if (player == ayer1)
        // {
        //     FriendlySide.Add(card);
        // }
        // else
        // {
        //     EnemySide.Add(card);
        // }
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
