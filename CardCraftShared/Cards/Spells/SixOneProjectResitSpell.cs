namespace CardCraftShared.Cards.Spells;

public class SixOneProjectResitSpell : BaseSpell
{
    public SixOneProjectResitSpell() : base(
        4,
        "Project6.1 Resit",
        "Remove 2 Mana from enemy Hero",
        CardRarityEnum.COMMON,
        "sixoneprojectresitspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.RemoveMana(2);
    }
}