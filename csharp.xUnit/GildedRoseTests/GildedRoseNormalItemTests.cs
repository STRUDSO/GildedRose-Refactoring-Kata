using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseNormalItemTests_NormalItem_
{
    [Fact]
    public void SpecialItem_ShouldReplyNo()
    {
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.Sulfuras()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.BackStagePass()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.AgedBrie()));
        Assert.Equal(ProcessingResult.Handled, GildedRose.HandleNormalItem(Any.NormalItem()));
    }


    [Fact]
    public void DayPassed_ShouldDecreaseSellIn()
    {
        // Given
        var sellIn = 0;
        var item = Any.NormalItem(sellIn:sellIn);

        // When
        GildedRose.HandleNormalItem(item);

        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void DayPassed_QualityDecreased()
    {
        // Given
        var sellIn = 1;
        var quality = 10;
        var item = Any.NormalItem(sellIn:sellIn, quality:quality);

        // When
        GildedRose.HandleNormalItem(item);

        // Then
        Assert.Equal(quality - 1, item.Quality);
    }

    [Fact]
    public void DayPassed_QualityNotBelowZero()
    {
        // Given
        var item = Any.NormalItem();

        // When
        GildedRose.HandleNormalItem(item);

        // Then
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void SellInPassed_QualityTwiceAsFast()
    {
        var quality = 10;
        var item = Any.NormalItem(quality: quality);

        GildedRose.HandleNormalItem(item);

        Assert.Equal(quality - 2, item.Quality);
    }
}