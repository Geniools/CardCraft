namespace CardCraftShared.Core.Interfaces;

public interface IMinion : IBaseCard, IAttackable
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool CanAttack { get; set; }
    public event EventHandler OnDeath;
    public void TakeDamage(int damage);
    public void AttackMinion(IMinion minion);
    public void AttackHero(BaseHero hero);
    public void Damage(int damage);
    public void UnaliveSelf();
}