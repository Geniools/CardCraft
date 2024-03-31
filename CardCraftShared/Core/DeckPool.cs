using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class DeckPool : ICardStatsManager
{
    public Queue<IBaseCard> Cards { get; set; }
    public int MaxCards = 30;

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

    public IBaseCard DrawCard() 
    {
        if (IsEmpty()) throw new Exception("Deck is empty");
        var card = Cards.Dequeue();
        return card;
    }
    
    public void AddCard(IBaseCard card)
    {
        if (!IsFull()) Cards.Enqueue(card);
    }

    public bool IsEmpty()
    {
        return Cards.Count == 0;
    }
    
    public bool IsFull()
    {
        return Cards.Count == MaxCards;
    }

    public void AddDeck(IList<IBaseCard> deck)
    {
        if (deck.Count > MaxCards) throw new Exception("Deck is too large");
        foreach (var card in deck)
        {
            AddCard(card);
        }
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
