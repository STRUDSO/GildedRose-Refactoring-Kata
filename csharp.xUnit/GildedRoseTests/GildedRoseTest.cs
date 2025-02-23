using System;
using Xunit;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
}