using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal abstract class MinionEffectDecorator : IMinion
{
    private IMinion _minion;

    int IMinion.Health
    {
        get => _minion.Health;
        set => _minion.Health = value;
    }

    int IMinion.Attack
    {
        get => _minion.Attack;
        set => _minion.Attack = Math.Max(value, 0);
    }

    public int ManaCost { get; set; }
    public CardRarityEnum Rarity { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool CanAttack { get; set; }
    public string Image { get; set; }

    protected MinionEffectDecorator(IMinion minion)
    {
        this._minion = minion;
        this.ManaCost = minion.ManaCost;
        this.Rarity = minion.Rarity;
        this.Name = minion.Name;
        this.Description = minion.Description;
        this.Rarity = minion.Rarity;
        this.CanAttack = minion.CanAttack;
        this.Image = minion.Image;
    }

    public void AttackMinion(IMinion minion)
    {
        this._minion.AttackMinion(minion);
    }

    public void AttackHero(BaseHero hero)
    {
        this._minion.AttackHero(hero);
    }

    public virtual void TriggerEffect()
    {
        this._minion.TriggerEffect();
    }

    public virtual void Damage(int damage)
    {
        this._minion.Damage(damage);
    }

    public void TakeDamage(int damage)
    {
        this._minion.TakeDamage(damage);
    }
    
    protected virtual void DoSomethingForIBla()
    {
        
    }

}