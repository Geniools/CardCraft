using CardCraftShared;
using CardCraftShared.Cards.Heroes;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Cards.Spells;

namespace CardCraftClient.Service;

public class GameComponentsRegistration
{
    public IList<BaseHero> Heroes { get; init; }
    public IList<IBaseCard> Cards { get; init; }

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

        return heroes;
    }

    /// <summary>
    /// Register all the cards that must be available in the game here
    /// </summary>
    /// <returns>A list of all available cards in the game</returns>
    private List<IBaseCard> GetRegisteredCards()
    {
        List<IBaseCard> cards = new();

        cards.Add(new AlexCard());
        cards.Add(new AlexCard());
        cards.Add(new AlexCard());

        cards.Add(new TeoCard());
        cards.Add(new TeoCard());
        cards.Add(new TeoCard());

        cards.Add(new EvaldCard());
        cards.Add(new EvaldCard());
        cards.Add(new EvaldCard());

        cards.Add(new AndreiCard());
        cards.Add(new AndreiCard());
        cards.Add(new AndreiCard());

        cards.Add(new RobCard());
        cards.Add(new RobCard());
        cards.Add(new RobCard());

        cards.Add(new ArianCard());
        cards.Add(new ArianCard());
        cards.Add(new ArianCard());

        cards.Add(new CorvinCard());
        cards.Add(new CorvinCard());
        cards.Add(new CorvinCard());

        cards.Add(new JadynCard());
        cards.Add(new JadynCard());
        cards.Add(new JadynCard());

        cards.Add(new ResitSpell());
        cards.Add(new ResitSpell());
        cards.Add(new ResitSpell());

        cards.Add(new AmogusSpell());
        cards.Add(new AmogusSpell());
        cards.Add(new AmogusSpell());

        cards.Add(new TatedSpell());
        cards.Add(new TatedSpell());
        cards.Add(new TatedSpell());

        return cards;
    }
}