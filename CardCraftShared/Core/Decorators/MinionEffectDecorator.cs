using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal abstract class MinionEffectDecorator : IMinion
{
    private IMinion _minion;

    protected MinionEffectDecorator(IMinion minion)
    {
        this._minion = minion;
    }

    public void AttackMinion(IMinion minion)
    {
        throw new NotImplementedException();
    }

    public virtual void TriggerEffect()
    {
        this._minion.TriggerEffect();
    }
}