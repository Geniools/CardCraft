using CardCraftClient.ViewElements;
using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class GamePage : BasePage
{
    private DataTemplateSelector _itemTemplateSelector;

    public GamePage(GamePageViewModel vm) : base(vm)
    {
        InitializeComponent();

        this._itemTemplateSelector = new CardTemplateSelector
        {
            BaseMinionTemplate = this.BaseMinionTemplate,
            BaseSpellTemplate = this.BaseSpellTemplate
        };

        this.EnemyHand.ItemTemplate = this._itemTemplateSelector;
        this.EnemyBoardSide.ItemTemplate = this._itemTemplateSelector;

        this.CurrentPlayerHand.ItemTemplate = this._itemTemplateSelector;
        this.CurrentPlayerBoardSide.ItemTemplate = this._itemTemplateSelector;
    }
}

