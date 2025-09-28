namespace GildedRoseKata;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    public void Process()
    {
        GetUpdater().UpdateQuality(this);
    }

    private IUpdateQuality GetUpdater() => Name switch
    {
        "Aged Brie" => new AgedBrieUpdater(),
        "Backstage passes to a TAFKAL80ETC concert" => new BackStageUpdater(),
        "Sulfuras, Hand of Ragnaros" => new SulfurasUpdater(),
        _ => new OtherUpdater()
    };
}

public class AgedBrieUpdater : IUpdateQuality
{
    public void UpdateQuality(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality += 1;
        }

        item.SellIn -= 1;

        if (item.SellIn < 0 && item.Quality < 50)
        {
            item.Quality += 1;
        }
    }
}

public class BackStageUpdater : IUpdateQuality
{
    public void UpdateQuality(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality += 1;

            if (item.SellIn < 11 && item.Quality < 50)
            {
                item.Quality += 1;
            }

            if (item.SellIn < 6 && item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        item.SellIn -= 1;

        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
    }
}

public class SulfurasUpdater : IUpdateQuality
{
    public void UpdateQuality(Item item)
    {
    }
}

public interface IUpdateQuality
{
    void UpdateQuality(Item item);
}

public class OtherUpdater : IUpdateQuality
{
    public void UpdateQuality(Item item)
    {
        if (item.Quality > 0)
        {
            item.Quality -= 1;
        }

        item.SellIn -= 1;

        if (item.SellIn < 0 && item.Quality > 0)
        {
            item.Quality -= 1;
        }
    }
}