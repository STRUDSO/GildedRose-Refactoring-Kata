using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GildedRoseKata;

public class GildedRose
{
    private static readonly Action<Item> PipeLine =
        ToPipeline([
            HandleSulfuras,

            Do(item => item.SellIn -= 1),

            Do(HandleAgedBrie),
            Do(HandleBackStage),
            Do(HandleNormalItem),
            Do(HandleConjuredItem),

            Do(item =>
            {
                var ensureQualityRange = Math.Max(0, Math.Min(50, item.Quality));
                item.Quality = ensureQualityRange;
            })
        ]);

    private static Action<Item> ToPipeline(IEnumerable<Action<Item, Action<Item>>> actions)
    {
        // we need to start from the other end and build backwards...
        var reversed = actions.Reverse();

        // we create a chain where we end out doing a noop in the end
        var noop = (Item _) => { };

        // so ..._last(third_last(second_last(noop())))
        return reversed.Aggregate(noop, (next, current) => item => current(item, next));
    }

    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Backstage = "Backstage passes to a TAFKAL80ETC concert";
    private const string AgedBrie = "Aged Brie";
    private const string Conjured = "Conjured Mana Cake";

    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        this._items = items;
    }

    public void UpdateQuality()
    {
        foreach (var t in _items)
        {
            var qualityBefore = t.Quality;

            PipeLine(t);

            //Assertions
            Debug.Assert(0 <= t.Quality, "The Quality of an item is never negative");
            if (qualityBefore <=
                50) // Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a legendary item and as such its Quality is 80, and it never alters.
            {
                Debug.Assert(t.Quality <= 50, "The Quality of an item is never more than 50", t.ToString());
            }
        }
    }


    private static Action<Item, Action<Item>> Do(Action<Item> act)
    {
        return (it, next) =>
        {
            act(it);
            next(it);
        };
    }

    public static void HandleSulfuras(Item t, Action<Item> next)
    {
        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        if (t.Name == Sulfuras)
        {
            return;
        }

        next(t);
    }

    private static void HandleNormalItem(Item item)
    {
        HashSet<string> specialTreatment = [AgedBrie, Sulfuras, Backstage, Conjured];
        if (!specialTreatment.Contains(item.Name))
        {
            QualityDecay(-1)(item);
        }
    }

    private static void HandleConjuredItem(Item item)
    {
        if (item.Name == Conjured)
        {
            QualityDecay(-2)(item);
        }
    }

    private static void HandleAgedBrie(Item item)
    {
        if (item.Name == AgedBrie)
        {
            QualityDecay(1)(item);
        }
    }

    private static void HandleBackStage(Item item)
    {
        if (item.Name == Backstage)
        {
            item.Quality += item.SellIn switch
            {
                < 0 => -item.Quality,
                < 5 => 3,
                < 10 => 2,
                _ => 1
            };
        }
    }

    static Action<Item> QualityDecay(int qualityDelta)
    {
        return item =>
        {
            item.Quality += item.SellIn switch
            {
                < 0 => qualityDelta * 2,
                _ => qualityDelta
            };
        };
    }
}