using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class TatedSpell : BaseSpell
{
    public TatedSpell() : base
    (
        9,
        "Tated",
        "Tates 1 enemy minion, change their health and attack to 1",
        CardRarityEnum.EPIC,
        "tatedspell.jpg"
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
                minion.Health = 1;
                minion.Attack = 1;
                minion.Image = "tatedspell.jpg";
            }
        }
    }
}