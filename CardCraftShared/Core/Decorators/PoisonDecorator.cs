using System;
using System.Collections.Generic;
using System.Text;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators
{
    internal class PoisonDecorator(IMinion minion) : MinionEffectDecorator(minion)
    {
        public override void Damage(int damage)
        {
            minion.Attack = 9000;
            base.Damage(damage);
        }
    }
}
