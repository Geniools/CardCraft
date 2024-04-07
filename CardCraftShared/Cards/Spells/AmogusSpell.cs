using CardCraftShared.Cards.Minions;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared.Cards.Spells;

public class AmogusSpell : BaseSpell
{
    public AmogusSpell() : base
        (
            5,
            "Amogus",
            "Add one random card: Arian, Evald or Teo.",
            CardRarityEnum.RARE, 
            "amogusspell.jpg"
        ) { }

    public override void TriggerEffect(Player player, Player enemyPlayer, Board board)
    {
        var random = new Random();
        var randomCard = random.Next(1, 4);

        switch (randomCard)
        {
            case 1:
                player.Hand.Add(new ArianCard());
                break;
            case 2:
                player.Hand.Add(new EvaldCard());
                break;
            case 3:
                player.Hand.Add(new TeoCard());
                break;
        }
    }
}