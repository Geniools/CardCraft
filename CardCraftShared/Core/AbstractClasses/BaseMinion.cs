using CardCraftShared.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardCraftShared;

public abstract partial class BaseMinion : ObservableObject, IMinion
{
    [ObservableProperty]
    private int _health;

    partial void OnHealthChanged(int value)
    {
        if (this.Health <= 0)
        {
            UnaliveSelf();
        }
    }

    [ObservableProperty]
    private int _attack;

    partial void OnAttackChanged(int value)
    {
        if (this.Health <= 0)
        {
            UnaliveSelf();
        }
    }

    public event EventHandler OnDeath;

    public int ManaCost { get ; set; }
    public CardRarityEnum Rarity { get; init; }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _image;

    // CanAttack property is used to determine if the minion can attack
    [ObservableProperty]
    private bool _canAttack;

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

    public string Id { get; set; }

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

        // Generate a unique id for the minion
        Id = Guid.NewGuid().ToString();
    }

    public abstract void TriggerEffect(Player player, Player enemyPlayer, Board board);

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
        minion.Damage(Attack);
        TakeDamage(minion.Attack);
    }

    public void AttackHero(BaseHero hero)
    {
        hero.TakeDamage(_attack);
    }

    public void Damage(int damage)
    {
        Health -= damage;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void UnaliveSelf()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }
}
