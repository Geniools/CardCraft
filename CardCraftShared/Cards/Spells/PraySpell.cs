﻿namespace CardCraftShared.Cards.Spells;

public class PraySpell : BaseSpell
{
    public PraySpell(): base(
        2,
        "Pray",
        "Lord give me strength",
        CardRarityEnum.COMMON,
        "prayspell.png"
    ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        throw new NotImplementedException();
    }
}