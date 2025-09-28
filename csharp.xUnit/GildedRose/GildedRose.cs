using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly ReallyHardHttpService _service;

    public GildedRose(IList<Item> items)
    {
        _service = new ReallyHardHttpService(items);
    }


    public async Task UpdateQuality()
    {
        var items = await _service.GetItems();
        foreach (var t in items)
        {
            t.Process();
        }
        await SendItems(items.Select(x => x.ToBijson()));
    }

    private async Task SendItems(IEnumerable<string> items)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "http://foo.bar/bi-son/json/post");
        httpRequestMessage.Content = new StringContent(string.Join(Environment.NewLine, items), Encoding.UTF8, "application/json");
        await Task.Yield();
    }
}

public class ReallyHardHttpService
{
    private readonly IList<Item> _items;

    public ReallyHardHttpService(IList<Item> items)
    {
        _items = items;
    }

    public async Task<IEnumerable<Item>> GetItems()
    {
        await Task.Yield();
        return _items;

    }
}