namespace CardCraftShared.Core.Interfaces;

internal interface ICardStatsManager
{
    void DamageAllMinions(int damage);
    void HealAllMinions(int heal);
    void DamageMinion(IBaseCard minion, int damage);
    void HealMinion(IBaseCard minion, int heal);
}
