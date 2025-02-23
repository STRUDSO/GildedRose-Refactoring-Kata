using GildedRoseKata;

namespace GildedRoseTests;

public class Any
{
    public static Item NormalItem(string name ="foo", int sellIn = 0, int quality = 0)
    {
        return new Item { Name = name, SellIn = sellIn, Quality = quality };
    }

    public static Item AgedBrie(int quality = 0, int sellIn = 0)
    {
        return NormalItem("Aged Brie", quality:quality, sellIn:sellIn);
    }
}