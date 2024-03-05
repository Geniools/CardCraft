using CardCraftClient.View;

namespace CardCraftClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            
        }
    }
}
