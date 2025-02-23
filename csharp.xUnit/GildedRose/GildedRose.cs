using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        var next = () => Next(t);
        HandleSulfuras(t, next);

        static void Next(Item t)
        {
            List<Func<Item, ProcessingResult>> strategies =
            [
                HandleAgedBrie,
                HandleBackStage,
                HandleNormalItem,
                HandleConjuredItem
            ];
            if (strategies.All(strategy => strategy(t) != ProcessingResult.Handled))
            {
                throw new ApplicationException($"Cannot process {t}");
            }
        }
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

    public static ProcessingResult HandleNormalItem(Item item)
    {
        HashSet<string> specialTreatment = [AgedBrie, Sulfuras, Backstage, Conjured];
        return Process(item,
            () => !specialTreatment.Contains(item.Name),
            QualityDecay(-1));
    }

    public static ProcessingResult HandleConjuredItem(Item item)
    {
        return Process(item,
            () => item.Name == Conjured,
            QualityDecay(-2));
    }

    public static ProcessingResult HandleAgedBrie(Item item)
    {
        return Process(item,
            () => item.Name == AgedBrie,
            QualityDecay(1));
    }

    public static ProcessingResult HandleBackStage(Item item)
    {
        return Process(item,
            () => item.Name == Backstage,
            HandleBackStageQuality);

        static void HandleBackStageQuality(Item item)
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


    private static ProcessingResult Process(Item item, Func<bool> handles, Action<Item> qualityDecay)
    {
        if (!handles())
        {
            return ProcessingResult.No;
        }

        Action<Item>[] pipeline = [SellIn, qualityDecay, EnsureQualityIsInRange];
        foreach (var act in pipeline)
            act(item);
        return ProcessingResult.Handled;
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