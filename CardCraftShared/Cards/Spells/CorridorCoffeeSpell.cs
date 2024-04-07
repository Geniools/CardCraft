namespace CardCraftShared.Cards.Spells;

public class CorridorCoffeeSpell : BaseSpell
{
    public CorridorCoffeeSpell() : base(
        0,
        "Corridor Coffee",
        "TIME FOR COFFEE!!! \n Adds 2 Mana to your Hero",
        CardRarityEnum.COMMON,
        "corridorcoffeespell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveMana(2);
    }
}