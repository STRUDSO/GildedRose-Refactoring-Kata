using Xunit;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseAgedBrieTests
{
    [Fact]
    public void AgedBrie_Fresh_ShouldIncreaseInQuality()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality, sellIn: 1);

        UpdateQuality(item);

        Assert.Equal(quality + 1, item.Quality);
    }

    [Fact]
    public void AgedBrie_DaysPassed_ShouldIncreaseInQualityTwice()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality);

        UpdateQuality(item);

        Assert.Equal(quality + 2, item.Quality);
    }

    [Fact]
    public void AgedBrie_DayPassed_ShouldDecreaseSellIn()
    {
        var item = Any.AgedBrie();
        var sellIn = item.SellIn;

        UpdateQuality(item);

        Assert.Equal(sellIn - 1, item.SellIn);
    }

    private static void UpdateQuality(Item item)
    {
        new GildedRose([item]).UpdateQuality();
    }
}