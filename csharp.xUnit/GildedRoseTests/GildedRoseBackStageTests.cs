using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseBackStageTests {

    [Fact]
    public void Backstage_SellIn_ShouldDecrease()
    {
        var item = Any.BackStagePass();
        var sellIn = item.SellIn;

        UpdateQuality(item);


        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void BackStagePasses_MoreThan10Days_QualityBy1()
    {
        var quality = 10;
        var item = Any.BackStagePass(100, quality);

        UpdateQuality(item);

        Assert.Equal(quality + 1, item.Quality);
    }

    [Fact]
    public void BackStagePasses_10DaysOrLess_QualityBy2()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:10, quality:quality);

        UpdateQuality(item);

        Assert.Equal(quality + 2, item.Quality);
    }

    [Fact]
    public void BackStagePasses_5DaysOrLess_QualityBy3()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:5, quality:quality);

        UpdateQuality(item);

        Assert.Equal(quality + 3, item.Quality);
    }

    [Fact]
    public void BackStagePasses_5DaysOrLess_QualityTo0()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:0, quality:quality);

        UpdateQuality(item);

        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void BackstagePass_Quality50_ShouldNotIncrease()
    {
        var quality = 50;
        var item = Any.BackStagePass(sellIn:1, quality:quality);

        UpdateQuality(item);

        Assert.Equal(50, item.Quality);
    }

    private static void UpdateQuality(Item item)
    {
        new GildedRose([item]).UpdateQuality();
    }

}