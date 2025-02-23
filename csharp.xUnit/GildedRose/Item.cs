namespace GildedRoseKata;

public record Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }
}