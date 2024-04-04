using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public abstract class BaseMinion : IMinion, IAttackable
{
    private int _health;
    private int _attack;

    public int Attack
    {
        get => _attack;
        set => _attack = Math.Max(value, 0);
    }

    public int Health
    {
        get => _health;
        set => _health = Math.Max(value, 0);
    }
    public int ManaCost { get ; set; }
    public CardRarityEnum Rarity { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public string Image { get; init; }
    public bool CanAttack { get; set; }

    // Color and TextColor properties are used to determine the color of the card based on its rarity
    public string Color => Rarity switch
    {
        CardRarityEnum.COMMON => "#FFFFFF",
        CardRarityEnum.RARE => "#0070dd",
        CardRarityEnum.EPIC => "#a335ee",
        CardRarityEnum.LEGENDARY => "#ff8000",
        _ => "#FFFFFF"
    };

    public string TextColor => Rarity switch
    {
        CardRarityEnum.COMMON => "#000000",
        CardRarityEnum.RARE => "#FFFFFF",
        CardRarityEnum.EPIC => "#FFFFFF",
        CardRarityEnum.LEGENDARY => "#FFFFFF",
        _ => "#000000"
    };

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

    public void AttackTarget(IAttackable target)
    {
        switch (target)
        {
            case IMinion minion:
                AttackMinion(minion);
                break;
            case BaseHero hero:
                AttackHero(hero);
                break;
            default:
                throw new NotImplementedException();
        }

        CanAttack = false;
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
