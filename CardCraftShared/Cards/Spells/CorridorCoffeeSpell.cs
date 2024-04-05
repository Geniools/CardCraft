namespace CardCraftShared.Cards.Spells;

public class CorridorCoffeeSpell : BaseSpell
{
    public CorridorCoffeeSpell() : base(
        0,
        "Corridor Coffee",
        "Adds 2 Mana to the Hero",
        CardRarityEnum.COMMON,
        "corridorcoffeespell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveMana(2);
    }
}