namespace GildedRoseKata;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    public void Process()
    {
        var isAgedBrie = Name == "Aged Brie";
        var isBackstage = Name == "Backstage passes to a TAFKAL80ETC concert";
        var isSulf = Name == "Sulfuras, Hand of Ragnaros";

        if(isAgedBrie)
            DoAgedBrie();
        else if (isBackstage)
            DoBackStage();
        else if (isSulf)
            DoSulf();
        else
            Other();
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