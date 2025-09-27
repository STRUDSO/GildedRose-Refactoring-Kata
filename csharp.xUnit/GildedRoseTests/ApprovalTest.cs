using System.Collections.Generic;
using GildedRoseKata;
using System.Linq;
using System.Threading.Tasks;
using VerifyTests;
using VerifyXunit;

using Xunit;

namespace GildedRoseTests;

public class ApprovalTest
{
    [Fact]
    public Task Foo()
    {
        var verifySettings = new VerifySettings();
        //verifySettings.AutoVerify(true, false);
        IEnumerable<string> name = ["Foo", "Aged Brie", "Backstage passes to a TAFKAL80ETC concert", "Sulfuras, Hand of Ragnaros"];
        IEnumerable<int> sellin = [-1, 0, 1, 5,6, 7, 10, 11, 12, 42];
        IEnumerable<int> quality = [-1, 0, 1, 49, 50, 51];
        return Verifier.Combination(settings: verifySettings)
            .Verify(Act, name, sellin, quality);
    }

    private static Item Act(string name, int sellIn, int quality)
    {
        Item[] items = [new() { Name = name, SellIn = sellIn, Quality = quality }];
        GildedRose app = new GildedRose(items);
        app.UpdateQuality();
        return items.First();
    }
}