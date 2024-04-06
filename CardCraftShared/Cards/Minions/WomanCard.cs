﻿namespace CardCraftShared.Cards.Spells;

public class WomanCard : BaseMinion
{
    public WomanCard() : base(
        7,
            7,
        7,
        "A Woman",
        "Enemy is stunned, seeing woman for the first time in his life",
        CardRarityEnum.EPIC,
        "womancard.png"
    ) { }
    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
    }

}