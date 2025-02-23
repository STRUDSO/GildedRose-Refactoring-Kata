using System.Collections.Generic;
using System.Diagnostics;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var t in Items)
        {
            ProcessItem(t);

            //Assertions
            Debug.Assert(0 <= t.Quality, "The Quality of an item is never negative");
        }
    }

    private static void ProcessItem(Item t)
    {
        if (t.Name != "Aged Brie" && t.Name != "Backstage passes to a TAFKAL80ETC concert")
        {
            if (t.Quality > 0)
            {
                if (t.Name != "Sulfuras, Hand of Ragnaros")
                {
                    t.Quality = t.Quality - 1;
                }
            }
        }
        else
        {
            if (t.Quality < 50)
            {
                t.Quality = t.Quality + 1;

                if (t.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (t.SellIn < 11)
                    {
                        if (t.Quality < 50)
                        {
                            t.Quality = t.Quality + 1;
                        }
                    }

                    if (t.SellIn < 6)
                    {
                        if (t.Quality < 50)
                        {
                            t.Quality = t.Quality + 1;
                        }
                    }
                }
            }
        }

        if (t.Name != "Sulfuras, Hand of Ragnaros")
        {
            t.SellIn = t.SellIn - 1;
        }

        if (t.SellIn < 0)
        {
            if (t.Name != "Aged Brie")
            {
                if (t.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (t.Quality > 0)
                    {
                        if (t.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            t.Quality = t.Quality - 1;
                        }
                    }
                }
                else
                {
                    t.Quality = t.Quality - t.Quality;
                }
            }
            else
            {
                if (t.Quality < 50)
                {
                    t.Quality = t.Quality + 1;
                }
            }
        }
    }
}