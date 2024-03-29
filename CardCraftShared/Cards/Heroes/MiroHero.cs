﻿namespace CardCraftShared.Cards.Heroes;

public class MiroHero : BaseHero
{
    private const string Image = "mirohero.jpeg";
    private const string Name = "Miro";
    private const ColorEnum Color = ColorEnum.Black;
    private const string Description = "The hero Miro is a hardworking hero (no) that hates every programming language.";


    public MiroHero() : base(Color, Image, Name, Description)
    {
    }

    public MiroHero(int health) : base(Color, Image, Name, Description)
    {
        this.Health = health;
    }
}