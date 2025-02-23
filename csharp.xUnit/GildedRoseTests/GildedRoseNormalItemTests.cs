using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public abstract class GildedRoseSimpleItemTests(int qualityModifier = 1)
{
    protected abstract Item CreateItem(int sellIn = 0, int quality = 0);

    [Fact]
    public void DayPassed_ShouldDecreaseSellIn()
    {
        // Given
        var sellIn = 0;
        var item = CreateItem(sellIn);

        // When
        Handle(item);

        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }

    protected abstract void Handle(Item item);

    [Fact]
    public void DayPassed_QualityDecreased()
    {
        // Given
        var sellIn = 1;
        var quality = 10;
        var item = CreateItem(sellIn:sellIn, quality:quality);

        // When
        Handle(item);

        // Then
        Assert.Equal(quality - 1 * qualityModifier, item.Quality);
    }

    [Fact]
    public void DayPassed_QualityNotBelowZero()
    {
        // Given
        var item = CreateItem();

        // When
        Handle(item);

        // Then
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void SellInPassed_QualityTwiceAsFast()
    {
        var quality = 10;
        var item = CreateItem(quality: quality);

        Handle(item);

        Assert.Equal(quality - 2 * qualityModifier, item.Quality);
    }
}

public class GildedRoseSimpleItemTests_ConjuredItems() : GildedRoseSimpleItemTests(2)
{
    protected override Item CreateItem(int sellIn = 0, int quality = 0)
    {
        return Any.ConjuredItem(sellIn, quality);
    }

    [Fact]
    public void SellInQualityCloseToZero()
    {
        var conjuredItem = Any.ConjuredItem(1,4);

        Handle(conjuredItem);

        Assert.Equal(2, conjuredItem.Quality);
    }

    protected override void Handle(Item item)
    {
        GildedRose.HandleConjuredItem(item);
    }
}

public class GildedRoseSimpleItemTest_NormalItems : GildedRoseSimpleItemTests
{
    protected override Item CreateItem(int sellIn = 0, int quality = 0)
    {
        var item = Any.NormalItem(sellIn:sellIn, quality:quality);
        return item;
    }

    protected override void Handle(Item item)
    {
        GildedRose.HandleNormalItem(item);
    }

    [Fact]
    public void Normal_SpecialItem_ShouldReplyNo()
    {
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.Sulfuras()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.BackStagePass()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.AgedBrie()));
        Assert.Equal(ProcessingResult.No, GildedRose.HandleNormalItem(Any.ConjuredItem()));
        Assert.Equal(ProcessingResult.Handled, GildedRose.HandleNormalItem(Any.NormalItem()));
    }
}