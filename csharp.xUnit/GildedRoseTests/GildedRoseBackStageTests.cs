using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseBackStageTests {

    [Fact]
    public void BackstageHandles()
    {
        Assert.Equal(ProcessingResult.Handled, GildedRose.HandleBackStage(Any.BackStagePass()));

        Assert.Equal(ProcessingResult.No, GildedRose.HandleBackStage(Any.Sulfuras()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleBackStage(Any.AgedBrie()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleBackStage(Any.NormalItem()));
    }

    [Fact]
    public void Backstage_SellIn_ShouldDecrease()
    {
        var item = Any.BackStagePass();
        var sellIn = item.SellIn;

        GildedRose.HandleBackStage(item);


        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void BackStagePasses_MoreThan10Days_QualityBy1()
    {
        var quality = 10;
        var item = Any.BackStagePass(100, quality);

        GildedRose.HandleBackStage(item);

        Assert.Equal(quality + 1, item.Quality);
    }

    [Fact]
    public void BackStagePasses_10DaysOrLess_QualityBy2()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:10, quality:quality);

        GildedRose.HandleBackStage(item);

        Assert.Equal(quality + 2, item.Quality);
    }

    [Fact]
    public void BackStagePasses_5DaysOrLess_QualityBy3()
    {
        var quality = 0;
        var item = Any.BackStagePass(sellIn:5, quality:quality);

        GildedRose.HandleBackStage(item);

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

    [Fact]
    public void BackstagePass_Quality50_ShouldNotIncrease()
    {
        var quality = 50;
        var item = Any.BackStagePass(sellIn:1, quality:quality);

        GildedRose.HandleBackStage(item);

        Assert.Equal(50, item.Quality);
    }

}