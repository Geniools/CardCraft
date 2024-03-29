using System.Collections.ObjectModel;
using CardCraftClient.Service;
using CardCraftClient.View;
using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardCraftClient.ViewModel;

public partial class DeckPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<IBaseCard> _availableCards;

    [ObservableProperty]
    private ObservableCollection<IBaseCard> _deck;

    public DeckPageViewModel(GameComponentsRegistration gcr)
    {
        this.AvailableCards = new(gcr.Cards);
        this.Deck = new();
    }

    [RelayCommand]
    private async Task AddCard(IBaseCard card)
    {
        this.IsBusy = true;

        this.Deck.Add(card);

        this.IsBusy = false;
    }

    [RelayCommand]
    private async Task RemoveCard(IBaseCard card)
    {
        this.IsBusy = true;

        this.Deck.Remove(card);

        this.IsBusy = false;
    }

    [RelayCommand]
    private async Task SaveDeck()
    {
        Shell.Current.GoToAsync($"///{nameof(HeroPage)}");
    }
}