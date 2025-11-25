using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GildedRoseKata;

public class ReallyHardHttpService
{
    public async Task<IEnumerable<Item>> GetItems()
    {
        await Task.Yield();
        throw new Exception("HMMM Did you just launch....");
    }
}