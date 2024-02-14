namespace CardCraftShared;

public abstract class BaseHero
{
    private int _health;
    private string _color;

    public BaseHero(int health, string color)
    {
        _health = health;
        _color = color;
    }

    public void TriggerHeroPower()
    {
        throw new NotImplementedException();
    }
}
