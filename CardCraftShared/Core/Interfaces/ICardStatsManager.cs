namespace CardCraftShared.Core.Interfaces;

internal interface ICardStatsManager
{
    void DamageAllMinions(int damage);
    void HealAllMinions(int heal);
    void DamageMinion(IMinion minion, int damage);
    void HealMinion(IMinion minion, int heal);
}
