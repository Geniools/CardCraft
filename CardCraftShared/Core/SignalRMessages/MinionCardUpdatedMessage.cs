namespace CardCraftShared;

public class MinionCardUpdatedMessage
{
    public string SenderBoardSide { get; set; }
    public string Id { get; set; }

    public int Health { get; set; }
    public int Attack { get; set; }
    public int ManaCost { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}