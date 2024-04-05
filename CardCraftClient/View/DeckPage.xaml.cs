using CardCraftClient.ViewElements;
using CardCraftClient.ViewModel;

namespace CardCraftClient.View;

public partial class DeckPage : BasePage
{
    private DataTemplateSelector _itemTemplateSelector;

    public DeckPage(DeckPageViewModel vm) : base(vm)
    {
        InitializeComponent();

        // Create an instance of ItemTemplateSelector
        this._itemTemplateSelector = new CardTemplateSelector
        {
            BaseMinionTemplate = this.BaseMinionTemplate,
            BaseSpellTemplate = this.BaseSpellTemplate
        };

        // Set the ItemTemplateSelector property of the CollectionView
        this.AvailableCards.ItemTemplate = this._itemTemplateSelector;
        this.Deck.ItemTemplate = this._itemTemplateSelector;

        // Reset the data template when the collection changes - prevents a bug where the data template is not applied
        // This solution slows down the app A LOT (check the CardTemplateSelector for more information)

        // vm.AvailableCardsCollectionChangedAction += this.ResetAvailableCardCollectionViewTemplate;
        // vm.DeckCollectionChangedAction += this.ResetDeckCollectionViewTemplate;
    }

    private void ResetAvailableCardCollectionViewTemplate()
    {
        this.AvailableCards.ItemTemplate = null;
        this.AvailableCards.ItemTemplate = this._itemTemplateSelector;
    }

    private void ResetDeckCollectionViewTemplate()
    {
        this.Deck.ItemTemplate = null;
        this.Deck.ItemTemplate = this._itemTemplateSelector;
    }
}