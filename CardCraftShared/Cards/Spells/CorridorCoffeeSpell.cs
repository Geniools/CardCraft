namespace CardCraftShared.Cards.Spells;

public class CorridorCoffeeSpell : BaseSpell
{
    public CorridorCoffeeSpell() : base(
        1,
        "Corridor Coffee",
        "Sanity Restored",
        CardRarityEnum.COMMON,
        "corridorcoffeespell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveMana(2);
    }
}