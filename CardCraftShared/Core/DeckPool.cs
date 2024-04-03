using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class DeckPool : ICardStatsManager
{
    public Queue<IBaseCard> Cards { get; set; }
    public const int MAX_AMOUNT_CARDS = 30;

    public DeckPool()
    {
        this.Cards = [];
        this.Cards = new();
    }

    public DeckPool(IList<IBaseCard> cards)
    {
        if (cards.Count > MAX_AMOUNT_CARDS) throw new Exception("Deck is too large");
        this.Cards = new(cards);
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
        return Cards.Count == MAX_AMOUNT_CARDS;
    }

    public void AddDeck(IList<IBaseCard> deck)
    {
        if (deck.Count > MAX_AMOUNT_CARDS) throw new Exception("Deck is too large");
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
