using CardCraftShared.Cards.Minions;

namespace CardCraftShared.Cards.Spells;

public class SixOneProjectResitSpell : BaseSpell
{
    public SixOneProjectResitSpell() : base(
        4,
        "Project6.1 Resit",
        "Here we go again... \n If you have 15 or less health, add Alex to your hand.",
        CardRarityEnum.RARE,
        "sixoneprojectresitspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        if (player.Hero.Health <= 15)
        {
            player.Hand.Add(new AlexCard());
        }
    }
}