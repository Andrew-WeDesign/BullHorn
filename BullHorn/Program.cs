using BullHorn.ApiData;
using BullHorn.DbData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BullHorn
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            List<Ticker> symbols = new List<Ticker>();
            symbols = Symbols.GetSymbols();
            DailyAdjusted.GetDailyAdjusted(symbols).Wait();
        }

    }
}
