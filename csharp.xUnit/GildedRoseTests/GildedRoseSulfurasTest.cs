using System;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseSulfurasTest
{
    [Fact]
    public void Sulfuras__NeverDecreasesInSellInOrQuality()
    {
        var item = Any.Sulfuras();
        var itemQuality = item.Quality;
        var itemSellIn = item.SellIn;

        GildedRose.HandleSulfuras(item, _ => {} );

        Assert.Equal(itemQuality, item.Quality);
        Assert.Equal(itemSellIn, item.SellIn);
    }
}