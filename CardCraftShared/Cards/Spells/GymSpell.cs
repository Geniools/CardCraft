﻿namespace CardCraftShared.Cards.Spells;

public class GymSpell : BaseSpell
{
    public GymSpell() : base(
        3,
        "Gym",
        "What am i doing today? Chest and back",
        CardRarityEnum.COMMON,
        "gymspell.jpg"
    ) { }

    public override void Trigger(DeckPool deck, DeckPool enemyDeck, Board board, Board enemyBoard, BaseHero hero,
        BaseHero enemyHero)
    {
        throw new NotImplementedException();
    }
}