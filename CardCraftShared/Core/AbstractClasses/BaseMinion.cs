using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public abstract class BaseMinion : BaseCard, IMinion
{
    private int _health;
    private int _attack;

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
        this.Health = health;
        this.Attack = attack;
    }

    public void TriggerEffect()
    {
        throw new NotImplementedException();
    }

    public void AttackMinion(IMinion minion)
    {
        throw new NotImplementedException();
    }
}
