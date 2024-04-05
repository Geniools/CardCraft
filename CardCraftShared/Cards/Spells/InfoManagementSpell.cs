using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class InfoManagementSpell : BaseSpell
{
    public InfoManagementSpell() : base(
        6,
        "InfoManagement",
        "Remove 2 Attack points from each minion in enemy Hand",
        CardRarityEnum.RARE,
        "infomanagementspell.jpeg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        foreach (IBaseCard card in enemyPlayer.Hand.Cards)
        {
            if (card is IMinion minion)
            {
                minion.Attack -= 2;
            }
        }
    }
}