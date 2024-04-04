using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Core.Decorators;

internal class ChargeDecorator(IMinion minion) : MinionEffectDecorator(minion)
{
    bool _hasCharge = true;
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (_hasCharge)
        {
            CanAttack = true;
            _hasCharge = false;
        }
        base.TriggerEffect(player, enemyPlayer, board);
    }

    public override string UpdateDescription(string description)
    {
        return base.UpdateDescription("Charge");
    }
}