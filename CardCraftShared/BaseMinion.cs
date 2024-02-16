namespace CardCraftShared;

public abstract class BaseMinion : BaseCard
{
    private int _health;
    private int _attack;

    private List<SpecialStatus> SpecialStatuses { get; init; }
    
    public int Health
    {
        get => this._health;
        set {
            if (value >= 0)
            {
                this._health = value;
            }
            else
            {
                // TODO: Change to a more informative exception
                throw new Exception();
            }
        }
    }

    public int Attack
    {
        get => this._attack;
        set
        {
            if (value >= 0)
            {
                this._attack = value;
            }
            else
            {
                // TODO: Change to a more informative exception
                throw new Exception();
            }
        }
    }

    public BaseMinion(int manaCost, string name, string description, CardRarity rarity, int health, int attack) : base(manaCost, name, description, rarity)
    {
        this.SpecialStatuses = new();
        this.Health = health;
        this.Attack = attack;
    }
}
