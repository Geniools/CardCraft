using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class RushDecorator(IMinion minion) : MinionEffectDecorator(minion)
{
    bool _hasRush = true;
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (_hasRush)
        {
            CanAttack = true;
        }
        base.TriggerEffect( player, enemyPlayer, board);
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
    public override string UpdateDescription(string description)
    {
        return base.UpdateDescription("Rush");
    }

}