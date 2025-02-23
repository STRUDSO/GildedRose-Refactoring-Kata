﻿using Xunit;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseAgedBrieTests
{
    [Fact]
    public void AgedBrie_WrongName_ShouldSayNo()
    {
        var handleAgedBrie = GildedRose.HandleAgedBrie(Any.NormalItem());

        Assert.Equal(ProcessingResult.No, handleAgedBrie);
    }
    [Fact]
    public void AgedBrie_Fresh_ShouldIncreaseInQuality()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality, sellIn: 1);

        GildedRose.HandleAgedBrie(item);

        Assert.Equal(quality + 1, item.Quality);
    }

    [Fact]
    public void AgedBrie_DaysPassed_ShouldIncreaseInQualityTwice()
    {
        var quality = 0;
        var item = Any.AgedBrie(quality);

        new GildedRose([item]).UpdateQuality();

        Assert.Equal(quality + 2, item.Quality);
    }
}