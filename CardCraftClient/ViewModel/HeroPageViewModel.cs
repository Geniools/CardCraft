using CardCraftShared;
using CommunityToolkit.Mvvm.ComponentModel;


namespace CardCraftClient.ViewModel
{
    public partial class HeroPageViewModel : BaseViewModel
    {

        public HeroPageViewModel()
        {

        }

        public string SelectedHero { get; set; }

        public async Task OnHeroSelected(string selectedHero)
        {
            SelectedHero = selectedHero;
            // Display alert with selected hero
            await Application.Current.MainPage.DisplayAlert("Selected Hero", $"You selected: {SelectedHero}", "OK");
        }

    }
}
