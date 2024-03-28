using CardCraftShared;
using CardCraftShared.Cards.Heroes;

namespace CardCraftClient.Service;

public class GameComponentsRegistration
{
    public List<BaseHero> Heroes { get; init; }
    public List<IBaseCard> Cards { get; init; }

    public GameComponentsRegistration()
    {
        this.Heroes = this.GetRegisteredHeroes();
        this.Cards = this.GetRegisteredCards();
    }

    /// <summary>
    /// Register all the heroes that must be available in the game here
    /// </summary>
    /// <returns>A list of all available heroes in the game</returns>
    private List<BaseHero> GetRegisteredHeroes()
    {
        List<BaseHero> heroes = new();

        heroes.Add(new AlexHero());
        heroes.Add(new MiroHero());
        heroes.Add(new ChrisHero());
        heroes.Add(new ChrisHero());

        return heroes;
    }

    /// <summary>
    /// Register all the cards that must be available in the game here
    /// </summary>
    /// <returns>A list of all available cards in the game</returns>
    private List<IBaseCard> GetRegisteredCards()
    {
        List<IBaseCard> cards = new();

        return cards;
    }
}