namespace CardCraftShared;

public abstract class BaseCard
{
    private int _manaCost;

    public BaseCard(int manaCost, string name, string description, CardRarity rarity)
    {
        this.ManaCost = manaCost;
        this.Name = name;
        this.Description = description;
        this.Rarity = rarity;
    }

    public int ManaCost { 
        get => this._manaCost;
        set {
            if ( value < 0 && value >= 10)
            {
                this._manaCost = value;
            }
        }
    }

    public CardRarity Rarity { get; set; }

    public string Name { get; init; }

    public string Description { get; init; }

}

