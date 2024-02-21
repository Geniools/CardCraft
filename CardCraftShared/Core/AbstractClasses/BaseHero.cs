namespace CardCraftShared;

public abstract class BaseHero
{
    public string Color { get; init; }
    public string Image { get; init; }
    public string Name { get; init; }

    protected int Health { get; set; }

    protected BaseHero(int health, string color, string image, string name)
    {
        this.Health = health;
        this.Color = color;
        this.Image = image;
        this.Name = name;
    }

    public void TriggerHeroPower()
    {
        throw new NotImplementedException();
    }
}
