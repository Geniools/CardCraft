using System.Collections.ObjectModel;
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
        if (this._signalRService.Player.Hero is not null)
        {
            this.SelectedHero = this._signalRService.Player.Hero;
        }
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
            if (this._signalRService.Player.Deck!.IsEmpty())
            {
                // bool answer = await Shell.Current.DisplayAlert("Warning!", "You have not made a deck! If you continue a random deck will be generated for you!", "Continue", "No");
                // if (!answer)
                // {
                //     return;
                // }

                await Shell.Current.DisplayAlert("Warning!", "You have not made a deck!", "Ok");
                return;
            }

            this._signalRService.Player.Hero = this.SelectedHero;

            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }
        else
        {
            Shell.Current.DisplayAlert("Error", "Please select a hero!", "Ok");
        }
    }
}
