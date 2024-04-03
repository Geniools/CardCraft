using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class DivineShield(IMinion minion) : MinionEffectDecorator(minion)
{
    private bool _hasDivineShield = true;

    public override void TakeDamage(int damage)
    {
        if (_hasDivineShield)
        {
            _hasDivineShield = false;
        }
        else
        {
            base.TakeDamage(damage);
        }
    }
}