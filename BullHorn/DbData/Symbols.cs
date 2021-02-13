using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullHorn.DbData
{
    class Symbols
    {
        public static List<Ticker> GetSymbols()
        {
            List<Ticker> symbols = new List<Ticker>();
            using (var context = new BullContext())
            {
                foreach (var ticker in context.Tickers)
                {
                    symbols.Add(ticker);

                    Console.WriteLine(ticker.Symbol);
                }
            }
            return symbols;
        }
    }
}
