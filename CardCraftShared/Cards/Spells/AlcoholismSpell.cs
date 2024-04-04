using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class AlcoholismSpell : BaseSpell
{
    public AlcoholismSpell() : base(
        6,
        "Alcoholism",
        "Casual Tuesday, gain 2 health",
        CardRarityEnum.RARE,
        "alcoholismspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        foreach (IBaseCard card in player.Hand.Cards)
        {
            if (card is IMinion minion)
            {
                minion.Attack += 2;
            }
        }
    }
}