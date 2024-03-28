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
    private BaseHero _selectedHero;

    public HeroPageViewModel(GameComponentsRegistration gcr, SignalRService signalRService)
    {
        this._signalRService = signalRService;
        this.Heroes = new(gcr.Heroes);
    }

    [RelayCommand]
    private async Task SelectHero(BaseHero hero)
    {
        this.IsBusy = true;
        this.SelectedHero = hero;
        this.IsBusy = false;
    }

    [RelayCommand]
    private async Task Select()
    {
        if (this.SelectedHero is not null)
        {
            this._signalRService.Player.Hero = this.SelectedHero;

            Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }
        else
        {
            Shell.Current.DisplayAlert("Error", "Please select a hero!", "Ok");
        }
    }
}
