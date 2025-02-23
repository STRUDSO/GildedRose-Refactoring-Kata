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
        var sellIn = 0;
        var item = new Item { Name = "foo", SellIn = sellIn, Quality = 0 };
        IList<Item> items = [item];
        GildedRose app = new GildedRose(items);
        app.UpdateQuality();

        Assert.Equal(sellIn - 1, item.SellIn);
    }
}