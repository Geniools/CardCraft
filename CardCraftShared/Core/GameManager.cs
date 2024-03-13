using CardCraftClient.Service;
using CardCraftShared.Cards.Minions;
using CardCraftShared.Core.Decorators;
using CardCraftShared.Core.Interfaces;

namespace CardCraftShared;

public class GameManager
{
    private readonly SignalRService _signalRService;

    private  List<Player> Players { get; init; }
    private Board Board { get; init; }
    private Graveyard Graveyard {  get; init; }

    public GameManager(SignalRService signalRService)
    {
        this._signalRService = signalRService;

        this.Players = new();
        this.Board = new();
        this.Graveyard = new();
    }
    
    public void StartGame()
    {
        this._signalRService.StartConnection();
    }

    public void EndGame()
    {
        this._signalRService.StopConnection();
    }

    public void AddPlayer()
    {

    }
}
