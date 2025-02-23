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
    private static readonly HashSet<string> SpecialTreatment = [AgedBrie, Sulfuras, Backstage];

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
        List<Func<Item, ProcessingResult>> strategies = [HandleSulfuras, HandleNormalItem];
        if (strategies.Any(strategy => strategy(t) == ProcessingResult.Handled))
        {
            return;
        }

        bool isBackStage = IsNamed(t, Backstage);
        bool isAgedBrie = IsNamed(t, AgedBrie);



        if (t.Quality < 50)
        {
            t.Quality += 1;

            if (isBackStage)
            {
                if (t.SellIn < 11)
                {
                    if (t.Quality < 50)
                    {
                        t.Quality += 1;
                    }
                }

                if (t.SellIn < 6)
                {
                    if (t.Quality < 50)
                    {
                        t.Quality += 1;
                    }
                }
            }
        }

        t.SellIn -= 1;

        if (t.SellIn < 0)
        {
            if (isAgedBrie)
            {
                if (t.Quality < 50)
                {
                    t.Quality += 1;
                }
            }
            else if (isBackStage)
            {
                t.Quality = 0;
            }
        }
    }

    private static bool IsNamed(Item item, string name)
    {
        return item.Name == name;
    }

    public static ProcessingResult HandleSulfuras(Item t)
    {
        return t.Name == Sulfuras ?
            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            ProcessingResult.Handled : ProcessingResult.No;
    }

    public static ProcessingResult HandleNormalItem(Item t)
    {
        if (SpecialTreatment.Contains(t.Name))
        {
            return ProcessingResult.No;
        }

        t.Quality -= 1;
        t.SellIn -= 1;

        if (t.SellIn < 0)
        {
            t.Quality -= 1;
        }

        t.Quality = Math.Max(t.Quality, 0);
        return ProcessingResult.Handled;
    }

    public enum ProcessingResult
    {
        Handled,
        No
    }

    public static ProcessingResult HandleAgedBrie(Item item)
    {
        if (item.Name != AgedBrie)
            return ProcessingResult.No;

        item.Quality = Math.Min(50, item.Quality + 1);

        return ProcessingResult.Handled;
    }
}