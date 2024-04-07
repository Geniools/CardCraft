namespace CardCraftShared.Cards.Spells;

public class IotLabSpell : BaseSpell
{
    public IotLabSpell() : base(
        0,
        "IoT Lab",
        "Motivational workspace! \n Gain 2 more Mana this turn",
        CardRarityEnum.COMMON,
        "iotlabspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.Mana += 2;
    }
}