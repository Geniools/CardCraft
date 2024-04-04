using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators
{
    internal class PoisonDecorator(IMinion minion) : MinionEffectDecorator(minion)
    {
        public override void Damage(int damage)
        {
            base.Damage(9000);
        }
    }
}