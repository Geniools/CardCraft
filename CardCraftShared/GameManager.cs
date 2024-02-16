namespace CardCraftShared;

public class GameManager
{
    private  List<Player> Players { get; init; }
    private Board Board { get; init; }
    private Graveyard Graveyard {  get; init; }

    public GameManager()
    {
        this.Players = new();
        this.Board = new();
        this.Graveyard = new();
    }

    public void StartGame()
    {

    }

    public void EndGame()
    {

    }

    public void AddPlayer()
    {

    }
}
