using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class TauntDecorator(IMinion minion) : MinionEffectDecorator(minion)
{
    public override void TriggerEffect()
    {
    }
}