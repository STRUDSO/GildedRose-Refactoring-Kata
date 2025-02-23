using System;
using Xunit;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void SellInShouldDecrease()
    {
        // Given
        var sellIn = 0;
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = 0 };

        // When
        new GildedRose([item]).UpdateQuality();

        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void QualityShouldNotGoBelow0()
    {
        // Given
        var item = new Item { Name = "foo", SellIn = 0, Quality = 0 };

        // When
        new GildedRose([item]).UpdateQuality();

        // Then
        Assert.Equal(0, item.Quality);
    }
}