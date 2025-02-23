using System;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseSulfurasTest
{

    [Fact]
    public void Sulfuras_HandleStuff()
    {
        Assert.Equal(ProcessingResult.No, Wrap(GildedRose.HandleSulfuras, Any.NormalItem()));
        Assert.Equal(ProcessingResult.No, Wrap(GildedRose.HandleSulfuras, Any.BackStagePass()));
        Assert.Equal(ProcessingResult.No, Wrap(GildedRose.HandleSulfuras, Any.AgedBrie()));
        Assert.Equal(ProcessingResult.Handled, Wrap(GildedRose.HandleSulfuras, Any.Sulfuras()));
    }

    [Fact]
    public void Sulfuras__NeverDecreasesInSellInOrQuality()
    {
        var item = Any.Sulfuras();
        var itemQuality = item.Quality;
        var itemSellIn = item.SellIn;

        GildedRose.HandleSulfuras(item, () => {} );

        Assert.Equal(itemQuality, item.Quality);
        Assert.Equal(itemSellIn, item.SellIn);
    }

    private static ProcessingResult Wrap(Action<Item, Action> handleSulfuras, Item t)
    {
        var processing = ProcessingResult.Handled;
        handleSulfuras(t, () => processing = ProcessingResult.No);
        return processing;
    }
}