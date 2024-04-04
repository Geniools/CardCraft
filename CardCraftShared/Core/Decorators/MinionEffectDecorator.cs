using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal abstract class MinionEffectDecorator : IMinion
{
    private IMinion _minion;

    public event EventHandler OnDeath;

    public int Health
    {
        get => _minion.Health;
        set => _minion.Health = value;
    }

    public int Attack
    {
        get => _minion.Attack;
        set => _minion.Attack = Math.Max(value, 0);
    }

    public int ManaCost { get; set; }
    public CardRarityEnum Rarity { get; init; }
    public string Name { get; init; }
    public string Description { get; set; }
    public bool CanAttack { get; set; }
    public string Image { get; set; }
    public string TextColor { get; set; }
    public string Color { get; set; }

    protected MinionEffectDecorator(IMinion minion)
    {
        this._minion = minion;
        this.ManaCost = minion.ManaCost;
        this.Rarity = minion.Rarity;
        this.Name = minion.Name;
        this.Description = UpdateDescription(minion.Description);
        this.Rarity = minion.Rarity;
        this.CanAttack = minion.CanAttack;
        this.Image = minion.Image;

        if (minion is BaseMinion baseMinion)
        {
            this.TextColor = baseMinion.TextColor;
            this.Color = baseMinion.Color;
        }
    }

    public void AttackMinion(IMinion minion)
    {
        this._minion.AttackMinion(minion);
    }

    public void AttackHero(BaseHero hero)
    {
        this._minion.AttackHero(hero);
    }

    public virtual void Damage(int damage)
    {
        this._minion.Damage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        this._minion.TakeDamage(damage);
    }

    public virtual void AttackTarget(IAttackable target)
    {
        this._minion.AttackTarget(target);
    }

    public virtual string UpdateDescription(string description)
    {
        return "Special effect:" + description + "\n" + this._minion.Description;
    }

    public void UnaliveSelf()
    {
        this._minion.UnaliveSelf();
    }

    public virtual void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        this._minion.TriggerEffect(player, enemyPlayer, board);
    }
}