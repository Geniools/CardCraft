using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class DeckPool
{
    public const int MAX_AMOUNT_CARDS = 30;

    private Queue<IBaseCard> PersistentCards { get; set; }
    public Queue<IBaseCard> Cards { get; set; }

    public DeckPool()
    {
        this.Cards = new();
        this.PersistentCards = new();
    }

    public DeckPool(IList<IBaseCard> cards)
    {
        if (cards.Count > MAX_AMOUNT_CARDS) throw new Exception("Deck is too large");
        this.Cards = new(cards);
        this.PersistentCards = new(cards);
    }
    
    public void Shuffle()
    {
        var cards = Cards.ToList();
        var shuffled = cards.OrderBy(a => Guid.NewGuid()).ToList();
        Cards = new Queue<IBaseCard>(shuffled);
    }

    public void Update(IList<IBaseCard> cards)
    {
        if (cards.Count > MAX_AMOUNT_CARDS) throw new Exception("Deck is too large");
        Cards = new(cards);
        PersistentCards = new(cards);
    }

    public IBaseCard DrawCard() 
    {
        if (IsEmpty()) throw new Exception("Deck is empty");

        return Cards.Dequeue();
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
        return Cards.Count >= MAX_AMOUNT_CARDS;
    }

    public void Reset()
    {
        Cards = new(PersistentCards);
    }
}
