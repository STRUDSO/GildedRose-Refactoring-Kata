using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    private Dictionary<string, Action> actions;

    public Item()
    {
        actions = new()
        {
            {"Sulfuras, Hand of Ragnaros", DoSulf},
            {"Backstage passes to a TAFKAL80ETC concert", DoBackStage},
            {"Aged Brie", DoAgedBrie},
        };
    }

    public void Process()
    {
        actions.GetValueOrDefault(Name, Other)();
    }

    private void Other()
    {
        if (Quality > 0)
        {
            Quality -= 1;
        }

        SellIn -= 1;

        if (SellIn < 0 && Quality > 0)
        {
            Quality -= 1;
        }
    }

    private void DoSulf()
    {
    }

    private void DoBackStage()
    {
        if (Quality < 50)
        {
            Quality += 1;

            if (SellIn < 11 && Quality < 50)
            {
                Quality += 1;
            }

            if (SellIn < 6 && Quality < 50)
            {
                Quality += 1;
            }
        }

        SellIn -= 1;

        if (SellIn < 0)
        {
            Quality = 0;
        }
    }

    private void DoAgedBrie()
    {
        if (Quality < 50)
        {
            Quality += 1;
        }

        SellIn -= 1;

        if (SellIn < 0 && Quality < 50)
        {
            Quality += 1;
        }
    }
}