using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class AmogusSpell : BaseSpell
{
    public AmogusSpell() : base
        (
            5,
            "Amogus", 
            "Get control over an enemy minion",
            CardRarityEnum.RARE, 
            "amogusspell.jpg"
        ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (enemyPlayer.Hand.Cards.Count > 0)
        {
            Random random = new Random();
            int randomIndex = random.Next(0, enemyPlayer.Hand.Cards.Count);

            IBaseCard randomCard = enemyPlayer.Hand.Cards[randomIndex];
            
            if (randomCard is IMinion minion)
            {
                enemyPlayer.Hand.Remove(minion);
            }
        }
    }
}