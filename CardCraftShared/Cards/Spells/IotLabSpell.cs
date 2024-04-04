namespace CardCraftShared.Cards.Spells;

public class IotLabSpell : BaseSpell
{
    public IotLabSpell() : base(
        3,
        "IoT Lab",
        "Increased productivity, 2 extra mana to the hero",
        CardRarityEnum.COMMON,
        "iotlabspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}