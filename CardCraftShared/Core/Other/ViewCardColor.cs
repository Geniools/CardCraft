namespace CardCraftShared.Core.Other;

public struct ViewCardColor
{
    public static string GetColor(ColorEnum color)
    {
        return color switch
        {
            ColorEnum.Blue => "#0078D4",
            ColorEnum.Green => "#107C10",
            ColorEnum.Black => "Black",
            _ => "Primary"
        };
    }

    public static string GetTextColor(ColorEnum color)
    {
        return color switch
        {
            ColorEnum.Blue => "White",
            ColorEnum.Green => "White",
            ColorEnum.Black => "White",
            _ => "Black"
        };
    }
}