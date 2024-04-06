using System.Collections.ObjectModel;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand
{
    public const int MAX_HAND_SIZE = 10;

    public ObservableCollection<IBaseCard> Cards { get; set; }

    public Hand()
    {
        this.Cards = new();
    }

    public void Add(IBaseCard card)
    {
        // Check the hand size
        if (this.Cards.Count >= MAX_HAND_SIZE)
        {
            // Trigger the CollectionChanged event
            this.Cards.Add(card);
            this.Cards.Remove(card);
            return;
        }

        this.Cards.Add(card);
    }

    public IBaseCard Remove(IBaseCard card)
    {
        Cards.Remove(card);
        return card;
    }

    public void Update(IList<IBaseCard> cards)
    {
        this.Cards.Clear();

        // A delay must be added, otherwise the list is updated too quickly, resulting in double cards (this is just a guess)
        Task.Delay(300).Wait();

        foreach (IBaseCard card in cards)
        {
            this.Cards.Add(card);
        }
    }

    public void Clear()
    {
        this.Cards.Clear();
    }
}
