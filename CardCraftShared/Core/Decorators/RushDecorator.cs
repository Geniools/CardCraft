using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class RushDecorator(IMinion minion) : MinionEffectDecorator(minion)
{
    bool _hasRush = true;
    public override void TriggerEffect()
    {
        if (_hasRush)
        {
            CanAttack = true;
        }
        base.TriggerEffect();
    }

    public override void AttackTarget(IAttackable target)
    {
        if (_hasRush)
        {
            switch (target)
            {
                case IMinion minion:
                    AttackMinion(minion);
                    break;
                default:
                    throw new NotImplementedException();
            }
            _hasRush = false;
        }
        else
        {
            base.AttackTarget(target);
        }
    }
}