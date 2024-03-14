using CardCraftShared.Core.Interfaces;
using System.Collections.Generic;

namespace CardCraftShared;

public class DeckPool : ICardStatsManager
{
    public Queue<IBaseCard> Cards { get; set; }
    public int maxCards = 30;

    public DeckPool()
    {
        this.Cards = [];
    }
    
    public void Shuffle()
    {
        var cards = Cards.ToList();
        var shuffled = cards.OrderBy(a => Guid.NewGuid()).ToList();
        Cards = new Queue<IBaseCard>(shuffled);
    }

    public IBaseCard DrawMinion()
    {
        if (!IsEmpty())
        {
            IBaseCard card = Cards.Dequeue();
            return card;
        }
        throw new Exception("Deck is empty");
    }
    

    public void AddCard(IBaseCard card)
    {
        if (!IsFull()) Cards.Enqueue(card);
    }

    public bool IsEmpty()
    {
        if (Cards.Count == 0)
        {
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        if (Cards.Count == maxCards)
        {
            return true;
        }
        return false;
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
