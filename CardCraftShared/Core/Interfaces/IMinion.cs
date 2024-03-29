namespace CardCraftShared.Core.Interfaces;

public interface IMinion : IBaseCard
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public bool CanAttack { get; set; }
    public void TriggerEffect();

    public void AttackMinion(IMinion minion);

    public void AttackHero(BaseHero hero);

    public void TakeDamage(int damage);
    
    public void Damage(int damage);
}