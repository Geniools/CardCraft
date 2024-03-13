namespace CardCraftShared.Core.Interfaces;

public interface IMinion : IBaseCard
{
    public int Health { get; set; }
    public int Attack { get; set; }
    public void TriggerEffect();

    public void AttackMinion(IMinion minion);

    public void Damage(int damage);
}

