namespace CardCraftShared.Cards.Spells;

public class LoremIpsumSpell : BaseSpell
{
    public LoremIpsumSpell() : base(
        0,
        "Lorem Ipsum",
        "Give 1 Mana to Hero",
        CardRarityEnum.COMMON,
        "loremipsumspell.jpg"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        player.GiveMana(1);
    }
}