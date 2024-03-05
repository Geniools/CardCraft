using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class DivineShield(IMinion minion) : MinionEffectDecorator(minion)
{
    public override void TriggerEffect()
    {
        bool IsDivineAvailable = true;
    }
}

