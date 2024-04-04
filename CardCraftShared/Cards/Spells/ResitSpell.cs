using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class ResitSpell : BaseSpell
{
    public ResitSpell() : base(
        7,
        "Resit", 
        "Resit is a spell that destroys a student's sanity.", 
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
