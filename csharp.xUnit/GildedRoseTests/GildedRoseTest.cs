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
        GildedRose app = new GildedRose([item]);

        // When
        app.UpdateQuality();

        // Then
        Assert.Equal(sellIn - 1, item.SellIn);
    }
}