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

        cards.Add(new AndreiCard());
        cards.Add(new AndreiCard());
        cards.Add(new AndreiCard());
        cards.Add(new AndreiCard());

        cards.Add(new ArianCard());
        cards.Add(new ArianCard());
        cards.Add(new ArianCard());
        cards.Add(new ArianCard());

        cards.Add(new BakiTeoCard());
        cards.Add(new BakiTeoCard());
        cards.Add(new BakiTeoCard());

        cards.Add(new BuffJadynCard());
        cards.Add(new BuffJadynCard());
        cards.Add(new BuffJadynCard());

        cards.Add(new ChrisCard()); 
        cards.Add(new ChrisCard());
        
        cards.Add(new CorvinCard());
        cards.Add(new CorvinCard());
        cards.Add(new CorvinCard());
        cards.Add(new CorvinCard());

        cards.Add(new CrackedUpMathewCard());
        cards.Add(new CrackedUpMathewCard());
        cards.Add(new CrackedUpMathewCard());

        cards.Add(new DimitriCard());
        cards.Add(new DimitriCard());
        cards.Add(new DimitriCard());

        cards.Add(new EvaldCard());
        cards.Add(new EvaldCard());
        cards.Add(new EvaldCard());
        cards.Add(new EvaldCard());

        cards.Add(new JadynCard());
        cards.Add(new JadynCard());
        cards.Add(new JadynCard());
        cards.Add(new JadynCard());

        cards.Add(new MathewCard());
        cards.Add(new MathewCard());
        cards.Add(new MathewCard());
        cards.Add(new MathewCard());

        cards.Add(new MiroCard());
        cards.Add(new MiroCard());

        cards.Add(new NathanCard());
        cards.Add(new NathanCard());
        cards.Add(new NathanCard());
        cards.Add(new NathanCard());

        cards.Add(new RobCard());
        cards.Add(new RobCard());
        cards.Add(new RobCard());
        cards.Add(new RobCard());

        cards.Add(new SecurityGuyCard());
        cards.Add(new SecurityGuyCard());
        cards.Add(new SecurityGuyCard());

        cards.Add(new TeacherCard());
        cards.Add(new TeacherCard());

        cards.Add(new TeoCard());
        cards.Add(new TeoCard());
        cards.Add(new TeoCard());
        cards.Add(new TeoCard());

        cards.Add(new TerryCard());
        cards.Add(new TerryCard());
        cards.Add(new TerryCard());
        cards.Add(new TerryCard());

        cards.Add(new WomanCard());
        cards.Add(new WomanCard());


        cards.Add(new AlcoholismSpell());
        cards.Add(new AlcoholismSpell());

        cards.Add(new AmogusSpell());
        cards.Add(new AmogusSpell());

        cards.Add(new BelastingdienstSpell());
        cards.Add(new BelastingdienstSpell());

        cards.Add(new CodingBugSpell());
        cards.Add(new CodingBugSpell());

        cards.Add(new CorridorCoffeeSpell());
        cards.Add(new CorridorCoffeeSpell());

        cards.Add(new DutchHousing());
        cards.Add(new DutchHousing());

        cards.Add(new ExamSpell());
        cards.Add(new ExamSpell());

        cards.Add(new GymSpell());
        cards.Add(new GymSpell());

        cards.Add(new InfoManagementSpell());
        cards.Add(new InfoManagementSpell());

        cards.Add(new IotLabSpell());
        cards.Add(new IotLabSpell());

        cards.Add(new ITRelationshipSpell());
        cards.Add(new ITRelationshipSpell());

        cards.Add(new ITRelationshipSpellUpgraded());
        cards.Add(new ITRelationshipSpellUpgraded());

        cards.Add(new LandlordVisitSpell());
        cards.Add(new LandlordVisitSpell());

        cards.Add(new LoremIpsumSpell());
        cards.Add(new LoremIpsumSpell());

        cards.Add(new NiceArgumentSpell());
        cards.Add(new NiceArgumentSpell());

        cards.Add(new PraySpell());
        cards.Add(new PraySpell());

        cards.Add(new ResitSpell());
        cards.Add(new ResitSpell());

        cards.Add(new SevenSevenThreeSpell());
        cards.Add(new SevenSevenThreeSpell());

        cards.Add(new SixOneProjectResitSpell());
        cards.Add(new SixOneProjectResitSpell());

        cards.Add(new TatedSpell());
        cards.Add(new TatedSpell());

        return cards;
    }
}   