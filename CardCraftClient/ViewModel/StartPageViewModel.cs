using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardCraftClient.ViewModel;

public partial class StartPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _lobbyCode;

    [ObservableProperty]
    private string _gameName;

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private bool _isEnabled;

    public StartPageViewModel()
    {
        this.GameName = "Card Craft";
        this.Title = "Join or Create a Game:";
        this.IsEnabled = true;
        this.Username = "Chris";
        this.LobbyCode = "773";
    }
}