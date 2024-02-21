namespace CardCraftShared;

public abstract class BaseMinion : BaseCard
{
    private int _health;
    private int _attack;

    public List<SpecialStatusEnum> SpecialStatuses { get; init; }
    
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

    protected BaseMinion(int manaCost, string name, string description, CardRarityEnum rarity, int health, int attack) 
        : base(manaCost, name, description, rarity)
    {
        this.SpecialStatuses = new();
        this.Health = health;
        this.Attack = attack;
    }

    public void AddSpecialStatus(SpecialStatusEnum specialStatus)
    {
        this.SpecialStatuses.Add(specialStatus);
    }
}
