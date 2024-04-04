namespace CardCraftShared.Cards.Minions;

public class MiroCard : BaseMinion
{
    public MiroCard() : base(
        9,
        9,
        9,
        "Miro",
        "I hate XAML",
        CardRarityEnum.EPIC,
        "mirocard.jpg"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }
}