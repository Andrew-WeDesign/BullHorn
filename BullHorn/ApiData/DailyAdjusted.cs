using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Importer.AlphaVantage;
using Trady.Analysis.Extension;
using Trady.Core;
using Trady.Core.Period;

namespace BullHorn.ApiData
{
    public class DailyAdjusted
    {
        public static async Task GetDailyAdjusted(List<Ticker> symbols)
        {
            //to get enough data for 180 day SMA
            var dNow = DateTime.Today;
            var dThen = dNow.AddYears(-1);
            DateTime dYesterday;
            if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
            {
                dYesterday = DateTime.Today.AddDays(-1);
            }
            else if (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                dYesterday = DateTime.Today.AddDays(-2);
            }
            else if (DateTime.Now > DateTime.Today.AddHours(20))
            {
                dYesterday = DateTime.Today;
            }
            else
            {
                dYesterday = DateTime.Today.AddDays(-1);
            }
            
            using (var context = new BullContext())
            {
                foreach (Ticker symbol in symbols)
                {
                    DailyOHLC dbCheck = context.DailyOHLCs
                        .Where(x => x.UnqCode == $"{symbol.Symbol}{dYesterday}")
                        .FirstOrDefault();
                    if (dbCheck == null) //$"{symbol.Symbol}{dYesterday}"
                    {
                        IReadOnlyList<Trady.Core.Infrastructure.IOhlcv> candles;
                        var importerFull = new AlphaVantageImporter("1Q633XY7FRIQL1BT", OutputSize.full);
                        try
                        {
                            candles = await importerFull.ImportAsync(symbol.Symbol, dThen);
                        }
                        catch (CsvHelper.BadDataException ex)
                        {
                            Console.WriteLine($"error was thrown on symbol {symbol.Symbol}!!!!!!!!!!");
                            Console.WriteLine(ex.Message);
                            continue;
                        }
                        var sma9 = candles.Sma(9);
                        var sma180 = candles.Sma(180);
                        var ema20 = candles.Ema(20);
                        var rsi12 = candles.Rsi(12);

                        int cnt = candles.Count;
                        for (int i = 0; i < cnt; i++)
                        {
                            DailyOHLC ohlc = new DailyOHLC();
                            ohlc.UnqCode = $"{symbol.Symbol}{candles[i].DateTime.Date}";
                            var test = await context.DailyOHLCs
                                .Where(x => x.UnqCode == ohlc.UnqCode)
                                .FirstOrDefaultAsync();
                            if (test != null)
                            {
                                continue;
                            }
                            else
                            {
                                ohlc.Open = candles[i].Open;
                                ohlc.High = candles[i].High;
                                ohlc.Low = candles[i].Low;
                                ohlc.Close = candles[i].Close;
                                ohlc.Volume = candles[i].Volume;
                                ohlc.Date = candles[i].DateTime.Date;
                                ohlc.Symbol = symbol.Symbol;
                                if (sma9[i].Tick != null)
                                    ohlc.SMA9Daily = (decimal)sma9[i].Tick;
                                if (sma180[i].Tick != null)
                                    ohlc.SMA180Daily = (decimal)sma180[i].Tick;
                                if (ema20[i].Tick != null)
                                    ohlc.EMA20Daily = (decimal)ema20[i].Tick;
                                if (rsi12[i].Tick != null)
                                    ohlc.RSI12Daily = (decimal)rsi12[i].Tick;
                                context.DailyOHLCs.Add(ohlc);
                                context.SaveChanges();

                                //Console.WriteLine(symbol.Symbol);
                                //Console.WriteLine(candles[i].Close);

                            }
                        }
                        Console.WriteLine($"Finished adding data for {symbol.Symbol}.");
                        await Task.Delay(1 * 12 * 1000);
                    }
                }
            }
        }
    }
}
