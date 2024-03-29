using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public abstract class BaseMinion : IMinion
{
    private int _health;
    private int _attack;

    int IMinion.Attack
    {
        get => _attack;
        set => _attack = Math.Max(value, 0);
    }

    int IMinion.Health
    {
        get => _health;
        set => _health = Math.Max(value, 0);
    }
    public int ManaCost { get ; set; }
    public CardRarityEnum Rarity { get; set; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; set; }
    public bool CanAttack { get; set; }

    public BaseMinion(int health, int attack, int manaCost, string name, string description, CardRarityEnum rarity, string image)
    {
        _health = health;
        _attack = attack;
        ManaCost = manaCost;
        Name = name;
        Description = description;
        Rarity = rarity;
        CanAttack = false;
        Image = image;
    }

    public void TriggerEffect()
    {
        throw new NotImplementedException();
    }

    public void AttackMinion(IMinion minion)
    {
        minion.Damage(_attack);
        TakeDamage(minion.Attack);
    }

    public void AttackHero(BaseHero hero)
    {
        hero.Health -= _attack;
    }

    public void Damage(int damage)
    {
        _health -= damage;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
