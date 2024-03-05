namespace CardCraftShared.Core.Interfaces;

public interface IMinion
{
    void TriggerEffect();

    public void AttackMinion(IMinion minion);
}

