using Xunit;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void NormalItem_DayPassed_ShouldDecreaseSellIn()
    {
        // Given
        var sellIn = 0;
        var item = Any.NormalItem(sellIn:sellIn);

        // When
        new GildedRose([item]).UpdateQuality();

        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void NormalItem_DayPassed_QualityNotBelowZero()
    {
        // Given
        var item = Any.NormalItem();

        // When
        new GildedRose([item]).UpdateQuality();

        // Then
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void NormalItem_SellInPassed_QualityTwiceAsFast()
    {
        var quality = 10;
        var item = Any.NormalItem(quality: quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality - 2, item.Quality);
    }

    [Fact]
    public void AgedBrie_Fresh_ShouldIncreaseInQuality()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality, sellIn:1);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality + 1, item.Quality);
    }

    [Fact]
    public void AgedBrie_DaysPassed_ShouldIncreaseInQualityTwice()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality + 2, item.Quality);
    }

    [Fact]
    public void Sulfuras__NeverDecreasesInSellInOrQuality()
    {
        var item = Any.Sulfuras();
        var itemQuality = item.Quality;
        var itemSellIn = item.SellIn;

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(itemQuality, item.Quality);
        Assert.Equal(itemSellIn, item.SellIn);
    }


}