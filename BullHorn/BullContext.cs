using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BullHorn
{
    public class BullContext : DbContext
    {
        public DbSet<Ticker> Tickers { get; set; }
        public DbSet<DailyOHLC> DailyOHLCs { get; set; }
        public DbSet<BullList> BullLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("server=localhost;user=root;database=bullbot;port=3306;Connect Timeout=5");
    }

    public class Ticker
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
    }

    public class DailyOHLC
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string UnqCode { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public decimal SMA9Daily { get; set; }
        public decimal SMA180Daily { get; set; }
        public decimal EMA20Daily { get; set; }
        public decimal RSI12Daily { get; set; }
        public decimal MacdLine { get; set; }
        public decimal SignalLine { get; set; }
        public decimal MacdHistogram { get; set; }
        public decimal UpperBand { get; set; }
        public decimal MiddleBand { get; set; }
        public decimal LowerBand { get; set; }
        public decimal Obv { get; set; }
    }
    public class BullList
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Reason { get; set; }
        public DateTime DateAdded { get; set; }
    }

}
