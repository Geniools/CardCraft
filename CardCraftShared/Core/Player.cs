namespace CardCraftShared;

public class Player
{
    public Player()
    {
        this.Hand = new Hand();
        this.Deck = new DeckPool();
        this.PlayerSignalRDetails = new PlayerSignalRDetails();
        this.Mana = 1;
    }

    // Connection-related properties
    public string? ConnectionId { get; set; }
    public string Name { get; set; }

    // This property contains details that need to be sent via SignalR
    // Because of SignalR limitations,  the whole Player object cannot be sent
    public PlayerSignalRDetails PlayerSignalRDetails { get; set; }

    // Game-related properties
    public BaseHero? Hero { get; set; }
    public DeckPool Deck { get; set; }
    public Hand Hand { get; set; }

    private int _mana;
    public int Mana
    {
        get => this._mana;
        set
        {
            if (value >= 0)
            {
                this._mana = value;
                this.OnManaChanged?.Invoke(this._mana);
            }
        }
    }
    public Action<int> OnManaChanged;

    public IBaseCard PlayCard(IBaseCard card)
    {
        return this.Hand.Remove(card);
    }

    public void DrawCard()
    {
        if (!Deck.IsEmpty())
        {
            IBaseCard card = Deck.DrawCard();
            Hand.Add(card);
        }
    }

    public void GiveHealth(int health)
    {
        Hero.Health += health;
    }

    public void GiveMana(int amount)
    {
        this.Mana += amount;
    }

    public void RemoveMana(int amount)
    {
        this.Mana -= amount;
    }
}
