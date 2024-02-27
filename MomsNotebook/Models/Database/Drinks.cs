using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Drinks : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public string DrinkText { get; set; }
        public double Quantity { get; set; }
    }
}
