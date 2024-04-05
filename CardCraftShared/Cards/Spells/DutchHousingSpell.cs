namespace CardCraftShared.Cards.Spells;

public class DutchHousingSpell : BaseSpell
{
    public DutchHousingSpell(): base(
        3,
        "Dutch housing",
        "Deal 2 damage to enemy Hero",
        CardRarityEnum.COMMON,
        "rentedroomspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        enemyPlayer.Hero.TakeDamage(2);
    }
}