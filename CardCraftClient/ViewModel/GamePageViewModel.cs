using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Model;
using CardCraftClient.Service;
using CardCraftShared.Core.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace CardCraftClient.ViewModel;

public partial class GamePageViewModel : BaseViewModel
{
    private readonly GameManager _gameManager;
    private readonly SignalRService _signalRService;

    [ObservableProperty] private int _timer;
    [ObservableProperty] private string _turnStatus;
    [ObservableProperty] private string _statusMessage;
    [ObservableProperty] private bool _isCurrentTurn;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsTemporaryCentredDisplayedCardVisible))] private BaseSpell _temporaryCentredDisplayedCard;

    partial void OnTemporaryCentredDisplayedCardChanged(BaseSpell? value)
    {
        // Display the card for 2 seconds
        if (value is not null)
        {
            Task.Delay(2000).ContinueWith(_ =>
            {
                this.TemporaryCentredDisplayedCard = null;
            });
        }
    }
    public bool IsTemporaryCentredDisplayedCardVisible => this.TemporaryCentredDisplayedCard is not null;

    // Current Player
    [ObservableProperty] private int _availableMana;
    [ObservableProperty] private Player _currentPlayer;
    [ObservableProperty] private int _currentPlayerHeroHealth;
    [ObservableProperty] private int _currentPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _currentPlayerHand;
    [ObservableProperty] private IBaseCard _selectedHandCard;
    [ObservableProperty] private ObservableCollection<BaseMinion> _currentPlayerBoard;
    [ObservableProperty] private BaseMinion _selectedFriendlyBoardCard;

    partial void OnSelectedFriendlyBoardCardChanged(BaseMinion? value)
    {
        // If the minion cannot attack, deselect it
        if (value is not null && !value.CanAttack)
        {
            this.SelectedFriendlyBoardCard = null;
        }
    }

    // Enemy Player
    [ObservableProperty] private Player _enemyPlayer;
    [ObservableProperty] private int _enemyPlayerHeroHealth;
    [ObservableProperty] private int _enemyPlayerDeckCardCount;
    [ObservableProperty] private ObservableCollection<IBaseCard> _enemyPlayerHand;
    [ObservableProperty] private ObservableCollection<BaseMinion> _enemyPlayerBoard;
    [ObservableProperty] private BaseMinion _selectedEnemyBoardCard;

    public GamePageViewModel(GameManager gm, SignalRService signalRService)
    {
        this._gameManager = gm;
        this._signalRService = signalRService;

        this.Title = "Card Craft";

        this.CurrentPlayer = gm.CurrentPlayer;
        this.EnemyPlayer = gm.EnemyPlayer;

        // Subscribe to game manager events
        gm.CurrentPlayer.Hand.Cards.CollectionChanged += async (sender, e) =>
        {
            // this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
            // this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;
            this.CurrentPlayerHand = this.CurrentPlayer.Hand.Cards;
            this.CurrentPlayerDeckCardCount = this.CurrentPlayer.Deck.Cards.Count;

            // Make the message to be sent to the enemy player
            // Notify the server
            await this._signalRService.SendCardAmountUpdateEnemyPlayer(new EnemyPlayerCardAmountUpdateMessage()
            {
                PlayerHandCardAmount = this.CurrentPlayerHand.Count,
                PlayerDeckCardAmount = this.CurrentPlayerDeckCardCount
            });
        };

        gm.EnemyPlayer.Hand.Cards.CollectionChanged += (sender, e) =>
        {
            this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;
            this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;

            this.EnemyPlayerHand = this.EnemyPlayer.Hand.Cards;
            this.EnemyPlayerDeckCardCount = this.EnemyPlayer.Deck.Cards.Count;
        };

        gm.Board.FriendlySide.CollectionChanged += async (sender, e) =>
        {
            // Subscribe the enemy minions to the event when added to the board
            if (e.NewItems != null)
            {
                foreach (BaseMinion minion in e.NewItems)
                {
                    minion.PropertyChanged += OnFriendlyMinionStatsChanged;
                }
            }

            this.CurrentPlayerBoard = gm.Board.FriendlySide;
        };

        gm.Board.EnemySide.CollectionChanged += (sender, e) =>
        {
            // Subscribe the enemy minions to the event when added to the board
            if (e.NewItems != null)
            {
                foreach (BaseMinion minion in e.NewItems)
                {
                    minion.PropertyChanged += OnEnemyMinionStatsChanged;
                }
            }

            this.EnemyPlayerBoard = gm.Board.EnemySide;
        };

        gm.CurrentPlayer.OnManaChanged += async (int newManaValue) =>
        {
            this.AvailableMana = newManaValue;
        };

        gm.OnTurnTimerChanged += async (int newTimerValue) =>
        {
            this.Timer = newTimerValue;
        };

        gm.OnCurrentTurnChanged += async (bool isCurrentTurn) =>
        {
            this.IsCurrentTurn = isCurrentTurn;

            this.TurnStatus = isCurrentTurn ? "Your Turn" : "Enemy's Turn";
        };

        gm.OnTemporaryCardDisplayAction += async (card) =>
        {
            this.TemporaryCentredDisplayedCard = card as BaseSpell;
        };

        // Hero health changed
        gm.CurrentPlayer.Hero.OnHealthChanged += async (int newHealthValue) =>
        {
            if (this.CurrentPlayerHeroHealth == newHealthValue)
            {
                return;
            }

            this.CurrentPlayerHeroHealth = newHealthValue;

            // Notify the server
            await this._signalRService.SendHeroUpdateEnemyPlayer(new EnemyPlayerHeroUpdateMessage()
            {
                SenderEnemyHeroHealth = this.EnemyPlayerHeroHealth,
                SenderFriendlyHeroHealth = newHealthValue
            });
        };

        gm.EnemyPlayer.Hero.OnHealthChanged += async (int newHealthValue) =>
        {
            if (this.EnemyPlayerHeroHealth == newHealthValue)
            {
                return;
            }

            this.EnemyPlayerHeroHealth = newHealthValue;

            // Notify the server
            await this._signalRService.SendHeroUpdateEnemyPlayer(new EnemyPlayerHeroUpdateMessage()
            {
                SenderEnemyHeroHealth = newHealthValue,
                SenderFriendlyHeroHealth = this.CurrentPlayerHeroHealth
            });
        };

        // Set initial values
        this.CurrentPlayerHand = gm.CurrentPlayer.Hand.Cards;
        this.EnemyPlayerHand = gm.EnemyPlayer.Hand.Cards;

        this.CurrentPlayerBoard = gm.Board.FriendlySide;
        this.EnemyPlayerBoard = gm.Board.EnemySide;

        this.AvailableMana = gm.CurrentPlayer.Mana;

        this.CurrentPlayerHeroHealth = gm.CurrentPlayer.Hero.Health;
        this.EnemyPlayerHeroHealth = gm.EnemyPlayer.Hero.Health;

        this.CurrentPlayerDeckCardCount = gm.CurrentPlayer.Deck.Cards.Count;
        this.EnemyPlayerDeckCardCount = gm.EnemyPlayer.Deck.Cards.Count;

        // Send initial values to the server
        _ = this._signalRService.SendCardAmountUpdateEnemyPlayer(new EnemyPlayerCardAmountUpdateMessage()
        {
            PlayerHandCardAmount = this.CurrentPlayerHand.Count,
            PlayerDeckCardAmount = this.CurrentPlayerDeckCardCount
        });

        this.Timer = gm.TurnTimer;
        this.TurnStatus = gm.IsCurrentTurn ? "Your Turn" : "Enemy's Turn";
        this.IsCurrentTurn = gm.IsCurrentTurn;

        // Start the turn if it is the first turn
        if (gm.IsCurrentTurn)
        {
            _ = gm.NextTurn();
        }
    }

    [RelayCommand]
    private async Task EndTurn()
    {
        // End the turn by changing the current turn to false (do not call EndTurn() => it will create a new Thread which will behave unexpectedly afterwards)
        this._gameManager.IsCurrentTurn = false;
    }

    [RelayCommand]
    private async Task PlayCard()
    {
        IBaseCard card = this.SelectedHandCard;

        // Check if there is a card to be played
        if (card is null)
        {
            await Shell.Current.DisplayAlert("Error", "No card to be played selected", "OK");
            return;
        }

        // Check if the player has enough mana to play the card
        if (this.AvailableMana < card.ManaCost)
        {
            await Shell.Current.DisplayAlert("Error", "Not enough mana to play the card", "OK");
            return;
        }

        try
        {
            // Remove the card from the player's hand
            this._gameManager.CurrentPlayer.PlayCard(card);

            // Decrease the player's mana
            this.AvailableMana -= card.ManaCost;

            // If the card is a minion, play it on the board
            if (card is BaseMinion minionCard)
            {
                this._gameManager.Board.PlayMinionFriendlySide(minionCard);
            }
            else
            {
                // Display it as a temporary card in the center of the screen
                this.TemporaryCentredDisplayedCard = card as BaseSpell;
            }

            // Trigger the card effect
            card.TriggerEffect(this._gameManager.CurrentPlayer, this._gameManager.EnemyPlayer, this._gameManager.Board);

            // Notify the server
            await this._signalRService.SendPlayedCardToEnemyPlayer(card);
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Oops :(", e.Message, "OK");

            // Revert the changes
            this.AvailableMana += card.ManaCost;

            // Add the card back to the player's hand
            this._gameManager.CurrentPlayer.Hand.Add(card);
        }
    }

    [RelayCommand]
    private async Task AttackMinion()
    {
        IMinion friendlyMinion = this.SelectedFriendlyBoardCard;
        IMinion enemyMinion = this.SelectedEnemyBoardCard;

        // Check if there are selected minions to attack
        if (friendlyMinion is null || enemyMinion is null)
        {
            await Shell.Current.DisplayAlert("Oops :(", "No minions selected to attack", "OK");
            return;
        }

        // Check if the friendly minion is able to attack
        if (!friendlyMinion.CanAttack)
        {
            await Shell.Current.DisplayAlert("Oops :(", $"Minion {friendlyMinion.Name} cannot attack", "OK");
            return;
        }

        // Attack the enemy minion
        friendlyMinion.AttackMinion(enemyMinion);

        // Disable the friendly minion from attacking again
        friendlyMinion.CanAttack = false;
    }

    [RelayCommand]
    private async Task AttackHero()
    {
        IMinion friendlyMinion = this.SelectedFriendlyBoardCard;

        // Check if there is a selected minion to attack
        if (friendlyMinion is null)
        {
            await Shell.Current.DisplayAlert("Oops :(", "No minion selected to attack", "OK");
            return;
        }

        // Check if the friendly minion is able to attack
        if (!friendlyMinion.CanAttack)
        {
            await Shell.Current.DisplayAlert("Oops :(", $"Minion {friendlyMinion.Name} cannot attack", "OK");
            return;
        }

        // Attack the enemy hero
        friendlyMinion.AttackHero(this._gameManager.EnemyPlayer.Hero);

        // Disable the friendly minion from attacking again
        friendlyMinion.CanAttack = false;
    }

    private void OnEnemyMinionStatsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is BaseMinion minion)
        {
            MinionCardUpdatedMessage message = new()
            {
                SenderBoardSide = "enemy",
                Id = minion.Id,
                Attack = minion.Attack,
                Health = minion.Health,
                Name = minion.Name,
                Description = minion.Description,
                Image = minion.Image
            };

            Trace.WriteLine($"Enemy Minion {minion.Name} stats updated: {message.Attack}/{message.Health}. Id: {minion.Id}");

            // Notify the server
            _ = this._signalRService.SendMinionUpdated(message);
        }
    }

    private void OnFriendlyMinionStatsChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is BaseMinion minion)
        {
            MinionCardUpdatedMessage message = new()
            {
                SenderBoardSide = "friendly",
                Id = minion.Id,
                Attack = minion.Attack,
                Health = minion.Health,
                Name = minion.Name,
                Description = minion.Description,
                Image = minion.Image
            };

            Trace.WriteLine($"Friendly Minion {minion.Name} stats updated: {message.Attack}/{message.Health}. Id: {minion.Id}");

            // Notify the server
            _ = this._signalRService.SendMinionUpdated(message);
        }
    }
}
