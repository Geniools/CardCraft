namespace CardCraftShared.Cards.Heroes;

public class ChrisHero : BaseHero
{
    private const string Image = "chrishero.jpeg";
    private const string Name = "Chris";
    private const ColorEnum Color = ColorEnum.Green;
    private const string Description = "The hero Chris is a handsome hero that can do XAML.";

    public ChrisHero() : base(DefaultHealth, ColorEnum.Green, Image, Name, Description)
    {
    }

    public ChrisHero(int health) : base(health, ColorEnum.Green, Image, Name, Description)
    {
    }
}