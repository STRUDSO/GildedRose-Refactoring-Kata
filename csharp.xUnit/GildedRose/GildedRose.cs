using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            if (qualityBefore <=
                50) // Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a legendary item and as such its Quality is 80, and it never alters.
            {
                Debug.Assert(t.Quality <= 50, "The Quality of an item is never more than 50", t.ToString());
            }
        }
    }

    private static void ProcessItem(Item t)
    {
        List<Func<Item, ProcessingResult>> strategies = [HandleSulfuras];
        if (strategies.Any(strategy => strategy(t) == ProcessingResult.Handled))
        {
            return;
        }

        Func<bool> isBackStage = HasName("Backstage passes to a TAFKAL80ETC concert", t);
        Func<bool> isAgedBrie = HasName("Aged Brie", t);


        var isRegularItem = !(isAgedBrie() || isBackStage());

        if (isRegularItem)
        {
            if (0 < t.Quality)
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
            if (isAgedBrie())
            {
                if (t.Quality < 50)
                {
                    t.Quality += 1;
                }
            }
            else if (isBackStage())
            {
                t.Quality = 0;
            }
            else if(isRegularItem)
            {
                if (0 < t.Quality)
                {
                    t.Quality -= 1;
                }
            }
        }
    }

    private static Func<bool> HasName(string name, Item item) => () => item.Name == name;

    private static ProcessingResult HandleSulfuras(Item t)
    {
        return t.Name == "Sulfuras, Hand of Ragnaros" ?
            // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            ProcessingResult.Handled : ProcessingResult.No;
    }

    public enum ProcessingResult
    {
        Handled,
        No
    }
}