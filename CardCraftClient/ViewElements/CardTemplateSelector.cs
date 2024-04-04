using CardCraftShared;

namespace CardCraftClient.ViewElements;

public class CardTemplateSelector : DataTemplateSelector
{
    public DataTemplate BaseSpellTemplate { get; set; }
    public DataTemplate BaseMinionTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        // Because of the issue with the DataTemplateSelector, only one DataTemplate should be used at a time, otherwise the selector will not be called
        // every time there is a change in the collection

        // Refer to the following link for more information:
        // https://stackoverflow.com/questions/74823748/datatemplateselector-is-not-executed-when-items-are-cleared-and-added-to-observa
        // The above solution slows down the app A LOT

        return item switch
        {
            BaseMinion => BaseMinionTemplate,
            // BaseSpell => BaseSpellTemplate,
            BaseSpell => BaseMinionTemplate,
            _ => BaseMinionTemplate
        };
    }
}