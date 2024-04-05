namespace CardCraftShared.Cards.Spells;

public class IotLabSpell : BaseSpell
{
    public IotLabSpell() : base(
        0,
        "IoT Lab",
        "Give 2 Mana to Hero",
        CardRarityEnum.COMMON,
        "iotlabspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.Mana += 2;
    }
}