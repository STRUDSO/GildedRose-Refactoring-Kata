using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GildedRoseKata;

public class GildedRose
{
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
            ProcessItem(t);

            //Assertions
            Debug.Assert(0 <= t.Quality, "The Quality of an item is never negative");
            if (qualityBefore <=
                50) // Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a legendary item and as such its Quality is 80, and it never alters.
            {
                Debug.Assert(t.Quality <= 50, "The Quality of an item is never more than 50", t.ToString());
            }
        }
    }

    private static void ProcessItem(Item t)
    {
        IEnumerable<Action<Item, Action>> actions = [
            HandleSulfuras,

            Do(SellIn),
                Do(HandleAgedBrie),
                Do(HandleBackStage),
                Do(HandleNormalItem),
                Do(HandleConjuredItem),
            Do(EnsureQualityIsInRange)

        ];

        Action next = null;
        using var enumerator = actions.GetEnumerator();
        next = () =>
        {
            if (enumerator.MoveNext())
            {
                enumerator.Current(t, next);
            }
        };
        next();
    }

    private static Action<Item, Action> Do(Action<Item> act)
    {
        return Do(it =>
        {
            act(it);
            return ProcessingResult.No;
        });
    }

    private static Action<Item, Action> Do(Func<Item, ProcessingResult> handleAgedBrie)
    {
        return (it, next) =>
        {
            handleAgedBrie(it);
            next();
        };
    }

    public static void HandleSulfuras(Item t, Action next)
    {
        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        if (t.Name == Sulfuras)
        {
            return;
        }

        next();
    }

    public static void HandleNormalItem(Item item)
    {
        HashSet<string> specialTreatment = [AgedBrie, Sulfuras, Backstage, Conjured];
        if (!specialTreatment.Contains(item.Name))
        {
            QualityDecay(-1)(item);
        }
    }

    public static void HandleConjuredItem(Item item)
    {
        if (item.Name == Conjured)
        {
            QualityDecay(-2)(item);
        }
    }

    public static void HandleAgedBrie(Item item)
    {
        if (item.Name == AgedBrie)
        {
            QualityDecay(1)(item);
        }
    }

    public static void HandleBackStage(Item item)
    {
        if (item.Name == Backstage)
        {
            item.Quality = item.SellIn switch
            {
                < 0 => 0,
                < 5 => item.Quality + 3,
                < 10 => item.Quality + 2,
                _ => item.Quality + 1
            };
        }
    }

    static Action<Item> QualityDecay(int qualityDelta)
    {
        return item =>
        {
            item.Quality = item.SellIn switch
            {
                < 0 => item.Quality + qualityDelta * 2,
                _ => item.Quality + qualityDelta
            };
        };
    }


    private static void SellIn(Item item)
    {
        item.SellIn -= 1;
    }

    private static void EnsureQualityIsInRange(Item item)
    {
        var ensureQualityRange = Math.Max(0, Math.Min(50, item.Quality));
        item.Quality = ensureQualityRange;
    }
}

public enum ProcessingResult
{
    Handled,
    No
}