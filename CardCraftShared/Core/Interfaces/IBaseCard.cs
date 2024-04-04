﻿namespace CardCraftShared;

public interface IBaseCard
{
    public int ManaCost { get; set; }
    public string Image { get; set; }

    public CardRarityEnum Rarity { get; init; }

    public string Name { get; init; }

    public string Description { get; set; }

    public void TriggerEffect(Player player, Player enemyPlayer, Board board);
}