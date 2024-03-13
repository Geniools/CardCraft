using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class DivineShield(IMinion minion) : MinionEffectDecorator(minion)
{
    bool hasDivineShield = true;

    public override void Damage(int damage)
    {
        if (hasDivineShield)
        {
            hasDivineShield = false;
        }
        else
        {
            base.Damage(damage);
        }
    }
}