using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseBackStageTests {

    [Fact]
    public void BackstageHandles()
    {
        Assert.Equal(ProcessingResult.Handled, GildedRose.HandleBackStage(Any.BackStagePass()));
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