using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class InfoManagementSpell : BaseSpell
{
    public InfoManagementSpell() : base(
        6,
        "InfoManagement",
        "We all remember this. Best subject ever... \n Deal 8 Damage to a random enemy minion",
        CardRarityEnum.RARE,
        "infomanagementspell.jpeg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (board.EnemySide.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, board.EnemySide.Count);

            IBaseCard randomCard = board.EnemySide[randomIndex];

            if (randomCard is IMinion minion)
            {
                minion.TakeDamage(7);
            }
        }
    }
}