using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public async Task foo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose();
        await app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
    }
}