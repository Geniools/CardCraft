using System.Collections.ObjectModel;
using System.Diagnostics;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class HeroPageViewModel : BaseViewModel
{
    private SignalRService _signalRService;

    [ObservableProperty] 
    private ObservableCollection<BaseHero> _heroes;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsHeroSelected))]
    private BaseHero _selectedHero;

    public bool IsHeroSelected => this.SelectedHero is not null;

    public HeroPageViewModel(GameComponentsRegistration gcr, SignalRService signalRService)
    {
        this._signalRService = signalRService;
        this.Heroes = new(gcr.Heroes);

        // Check if the player has a hero already

        // This is a workaround to set the selected hero after the page is loaded
        // https://stackoverflow.com/questions/75593079/programmatically-setting-the-selecteditem-of-a-collectionview-is-not-working-on/75598722#75598722
        _ = Task.Run(async () =>
        {
            await Task.Delay(100);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (this._signalRService.Player.Hero is not null)
                {
                    this.SelectedHero = this._signalRService.Player.Hero;
                    Trace.WriteLine($"Player has a hero already: {this.SelectedHero.Name}");
                }
            });
        });
    }

    [RelayCommand]
    private async Task BuildDeck()
    {
        this._signalRService.Player.Hero = this.SelectedHero;

        await Shell.Current.GoToAsync(nameof(DeckPage));
    }

    [RelayCommand]
    private async Task Select()
    {
        if (this.SelectedHero is not null)
        {
            this._signalRService.Player.Hero = this.SelectedHero;

            if (this._signalRService.Player.Deck is not null)
            {
                if (!this._signalRService.Player.Deck.IsEmpty())
                {
                    await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
                    return;
                }
            }

            await Shell.Current.DisplayAlert("Warning!", "You have not made a deck!", "Ok");
        }
        else
        {
            Shell.Current.DisplayAlert("Error", "Please select a hero!", "Ok");
        }
    }
}
