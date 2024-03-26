using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class HeroPage : BasePage
{
    public HeroPage(HeroPageViewModel vm) : base(vm)
    {
        InitializeComponent();
    }

    public HeroPageViewModel ViewModel { get; } = new HeroPageViewModel();

    private async void OnHeroSelected(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string selectedHero = (string)button.CommandParameter;
        await ViewModel.OnHeroSelected(selectedHero);
    }

}