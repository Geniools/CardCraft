namespace CardCraftShared.Core.Interfaces;

internal interface ICardStatsManager
{
    void DamageAllMinions(int damage);
    void HealAllMinions(int heal);
    void DamageMinion(BaseMinion minion, int damage);
    void HealMinion(BaseMinion minion, int heal);
}
