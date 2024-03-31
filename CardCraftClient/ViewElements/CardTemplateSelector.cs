using CardCraftShared;

namespace CardCraftClient.ViewElements;

public class CardTemplateSelector : DataTemplateSelector
{
    public DataTemplate BaseSpellTemplate { get; set; }
    public DataTemplate BaseMinionTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item switch
        {
            BaseMinion => BaseMinionTemplate,
            BaseSpell => BaseSpellTemplate,
            _ => throw new NotImplementedException()
        };
    }
}