using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly ReallyHardHttpService _service;

    public GildedRose()
    {
        _service = new ReallyHardHttpService();
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
        throw new Exception("Did you just send all info to the Drag On");
    }
}