using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell() : base(
        7,
        "Resit",
        "Remove 2 Health from each minion in enemy Hero hand", 
        CardRarityEnum.EPIC,
        "resitspell.jpeg") { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        foreach (IBaseCard card in enemyPlayer.Hand.Cards)
        {
            if (card is IMinion minion)
            {
                minion.Health -= 2;
            }
        }
    }
}
