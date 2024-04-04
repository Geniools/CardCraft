using System.Collections.ObjectModel;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class Hand : ICardStatsManager
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

        Task.Delay(50).Wait();

        foreach (IBaseCard card in cards)
        {
            this.Cards.Add(card);
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
