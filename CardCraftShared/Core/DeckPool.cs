using CardCraftShared.Core.Interfaces;
using System.Collections.Generic;

namespace CardCraftShared;

public class DeckPool : ICardStatsManager
{
    public List<IBaseCard> Cards { get; init; }
    public int maxCards = 30;

    public DeckPool()
    {
        this.Cards = [];
    }
    
    public void Shuffle()
    {
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = Random.Shared.Next(0, i + 1);
            (Cards[j], Cards[i]) = (Cards[i], Cards[j]);
        }
    }

    public IBaseCard DrawMinion()
    {
        if (!IsEmpty())
        {
            IBaseCard card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
        throw new Exception("Deck is empty");
    }
    

    public void AddCard(IBaseCard card)
    {
        if (!IsFull()) Cards.Add(card);
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
            throw new Exception("Deck is full");
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

    public void DamageMinion(IMinion minion, int damage)
    {
        throw new NotImplementedException();
    }

    public void HealMinion(IMinion minion, int heal)
    {
        throw new NotImplementedException();
    }
}
