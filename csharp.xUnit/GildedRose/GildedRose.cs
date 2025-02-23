using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GildedRoseKata;

public class GildedRose
{
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
            if (qualityBefore <= 50) // Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a legendary item and as such its Quality is 80, and it never alters.
            {
                Debug.Assert(t.Quality <= 50, "The Quality of an item is never more than 50", t.ToString());
            }
        }
    }

    private static void ProcessItem(Item t)
    {
        Func<bool> HasName(string name) => () => t.Name == name;
        Func<bool> isSulfuras = HasName("Sulfuras, Hand of Ragnaros");

        if (isSulfuras())
        {
            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            return;
        }

        Func<bool> isBackStage = HasName("Backstage passes to a TAFKAL80ETC concert");
        Func<bool> isAgedBrie = HasName("Aged Brie");


        if (!isAgedBrie() && !isBackStage())
        {
            if (t.Quality > 0)
            {
                t.Quality -= 1;
            }
        }
        else
        {
            if (t.Quality < 50)
            {
                t.Quality += 1;

                if (isBackStage())
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
        }

        t.SellIn -= 1;

        if (t.SellIn < 0)
        {
            PastSellDate(t, isAgedBrie, isBackStage);
        }
    }

    private static void PastSellDate(Item t, Func<bool> isAgedBrie, Func<bool> isBackStage)
    {
        if (isAgedBrie())
        {
            if (t.Quality < 50)
            {
                t.Quality += 1;
            }

            return;
        }

        if (isBackStage())
        {
            t.Quality -= t.Quality;
            return;
        }

        if (t.Quality > 0)
        {
            t.Quality -= 1;
        }
    }
}