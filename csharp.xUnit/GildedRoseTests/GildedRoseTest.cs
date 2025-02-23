using System;
using Xunit;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    private readonly int sellIn;
    private readonly Item item;

    public GildedRoseTest()
    {
        // Given
        sellIn = 0;
        item = new Item { Name = "foo", SellIn = sellIn, Quality = 0 };
        GildedRose app = new GildedRose([item]);

        // When
        app.UpdateQuality();
    }
    [Fact]
    public void SellInShouldDecrease()
    {
        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }

    [Fact]
    public void QualityShouldNotGoBelow0()
    {
        Assert.Equal(0, item.Quality);
    }
}