using CardCraftClient.ViewModel;

namespace CardCraftClient.View
{
    public partial class CardView : BasePage
    {
        public CardView() : this(new CardViewModel())
        {
        }

        public CardView(CardViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}