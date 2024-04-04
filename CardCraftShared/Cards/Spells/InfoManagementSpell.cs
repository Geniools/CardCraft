using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class InfoManagementSpell : BaseSpell
{
    public InfoManagementSpell() : base(
        6,
        "InfoManagement",
        "3ECs",
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