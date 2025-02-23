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

    public static Item Sulfuras()
    {
        const string name = "Sulfuras, Hand of Ragnaros";
        return NormalItem(name, quality:80);
    }

    public static Item BackStagePass(int sellIn = 0, int quality = 0)
    {
        const string name = "Backstage passes to a TAFKAL80ETC concert";
        return NormalItem(name, sellIn:sellIn, quality:quality);
    }

    public static Item ConjuredItem(int sellIn = 0, int quality = 0)
    {
        return NormalItem("Conjured Mana Cake", sellIn: sellIn, quality: quality);
    }
}