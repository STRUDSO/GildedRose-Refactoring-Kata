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
            if (qualityBefore <= 50) //Just for clarification, an item can never have its Quality increase above 50, however "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
            {
                Debug.Assert(t.Quality <= 50, "The Quality of an item is never more than 50", t.ToString());
            }
        }
    }

    private static void ProcessItem(Item t)
    {
        var isBackStage = t.Name == "Backstage passes to a TAFKAL80ETC concert";
        var notBackStage = !isBackStage;
        var notSulfuras = t.Name != "Sulfuras, Hand of Ragnaros";

        if (t.Name != "Aged Brie" && notBackStage)
        {
            if (t.Quality > 0)
            {
                if (notSulfuras)
                {
                    t.Quality -= 1;
                }
            }
        }
        else
        {
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
        }

        if (notSulfuras)
        {
            t.SellIn -= 1;
        }

        if (t.SellIn < 0)
        {
            if (t.Name != "Aged Brie")
            {
                if (notBackStage)
                {
                    if (t.Quality > 0)
                    {
                        if (notSulfuras)
                        {
                            t.Quality -= 1;
                        }
                    }
                }
                else
                {
                    t.Quality -= t.Quality;
                }
            }
            else
            {
                if (t.Quality < 50)
                {
                    t.Quality += 1;
                }
            }
        }
    }
}