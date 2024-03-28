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
}