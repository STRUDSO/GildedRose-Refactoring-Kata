using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public abstract class GildedRoseNormalItemTests_NormalItem_Base
{
    [Fact]
    public void SpecialItem_ShouldReplyNo()
    {
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.Sulfuras()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.BackStagePass()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.AgedBrie()));
        Assert.Equal(ProcessingResult.Handled, GildedRose.HandleNormalItem(Any.NormalItem()));
    }

    protected abstract Item CreateItem(int sellIn = 0, int quality = 0);

    [Fact]
    public void DayPassed_ShouldDecreaseSellIn()
    {
        // Given
        var sellIn = 0;
        var item = CreateItem(sellIn);

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
        var item = CreateItem(sellIn:sellIn, quality:quality);

        // When
        GildedRose.HandleNormalItem(item);

        // Then
        Assert.Equal(quality - 1, item.Quality);
    }

    [Fact]
    public void DayPassed_QualityNotBelowZero()
    {
        // Given
        var item = CreateItem();

        // When
        GildedRose.HandleNormalItem(item);

        // Then
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void SellInPassed_QualityTwiceAsFast()
    {
        var quality = 10;
        var item = CreateItem(quality: quality);

        GildedRose.HandleNormalItem(item);

        Assert.Equal(quality - 2, item.Quality);
    }
}

public class GildedRoseNormalItemTests_NormalItem_ : GildedRoseNormalItemTests_NormalItem_Base
{
    protected override Item CreateItem(int sellIn = 0, int quality = 0)
    {
        var item = Any.NormalItem(sellIn:sellIn, quality:quality);
        return item;
    }
}