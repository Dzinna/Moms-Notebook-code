using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Weights : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public double Weight { get; set; }
    }
}
