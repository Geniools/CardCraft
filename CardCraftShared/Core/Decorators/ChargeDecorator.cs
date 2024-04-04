using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class ChargeDecorator(IMinion minion) : MinionEffectDecorator(minion)
{
    bool _hasCharge = true;
    public override void TriggerEffect()
    {
        if (_hasCharge)
        {
            CanAttack = true;
            _hasCharge = false;
        }
        base.TriggerEffect();
    }

    public override string UpdateDescription(string description)
    {
        return base.UpdateDescription("Charge");
    }
}