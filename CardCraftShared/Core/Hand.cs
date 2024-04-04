using System.Collections.ObjectModel;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand
{
    public ObservableCollection<IBaseCard> Cards { get; set; }

    public Hand()
    {
        this.Cards = new();
    }

    public void Add(IBaseCard card)
    {
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

        foreach (IBaseCard card in cards)
        {
            this.Cards.Add(card);
        }
    }
}
