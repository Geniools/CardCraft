namespace CardCraftShared.Cards.Spells;

public class LandlordVisitSpell : BaseSpell
{
    public LandlordVisitSpell(): base(
        8,
        "Landlord visit",
        "Rent paid, time to eat potatoes for the left of the month... \n Destroy two random enemy minions.",
        CardRarityEnum.EPIC,
        "landlordvisitspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (board.EnemySide.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, board.EnemySide.Count);
            BaseMinion minion = board.EnemySide[randomIndex];
            minion.Health = 0;
        }

        if (board.EnemySide.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, board.EnemySide.Count);
            BaseMinion minion = board.EnemySide[randomIndex];
            minion.Health = 0;
        }
    }
}