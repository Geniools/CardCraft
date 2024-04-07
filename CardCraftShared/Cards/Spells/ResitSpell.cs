using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell() : base(
        7,
        "Resit",
        "I recall some memories. \n Remove an random enemy minion. Give its stats to a random friendly minion", 
        CardRarityEnum.EPIC,
        "resitspell.jpeg") { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (board.EnemySide.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, board.EnemySide.Count);

            BaseMinion enemyMinion = board.EnemySide[randomIndex];

            if (board.FriendlySide.Count > 0)
            {
                int randomIndex2 = random.Next(0, board.FriendlySide.Count);
                BaseMinion friendlyMinion = board.FriendlySide[randomIndex2];

                friendlyMinion.Attack += enemyMinion.Attack;
                friendlyMinion.Health += enemyMinion.Health;
            }

            // Kill the enemy minion
            enemyMinion.Health = 0;
        }
    }
}
