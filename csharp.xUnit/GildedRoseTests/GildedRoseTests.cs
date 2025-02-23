using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTests {

    [Fact]
    public void Sulfuras__NeverDecreasesInSellInOrQuality()
    {
        var item = Any.Sulfuras();
        var itemQuality = item.Quality;
        var itemSellIn = item.SellIn;

        GildedRose.HandleSulfuras(item);

        Assert.Equal(itemQuality, item.Quality);
        Assert.Equal(itemSellIn, item.SellIn);
    }

    [Fact]
    public void BackStagePasses_10DaysOrLess_QualityBy2()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:10, quality:quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality + 2, item.Quality);
    }

    [Fact]
    public void BackStagePasses_5DaysOrLess_QualityBy3()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:5, quality:quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality + 3, item.Quality);
    }

    [Fact]
    public void BackStagePasses_5DaysOrLess_QualityTo0()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:0, quality:quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(0, item.Quality);
    }

}