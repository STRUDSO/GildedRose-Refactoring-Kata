using GildedRoseKata;

namespace GildedRoseTests;

public class Any
{
    public static Item NormalItem(string name ="foo", int sellIn = 0, int quality = 0)
    {
        return new Item { Name = name, SellIn = sellIn, Quality = quality };
    }
}