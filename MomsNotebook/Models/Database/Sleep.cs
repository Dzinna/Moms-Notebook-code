using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Sleep : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        [NotNull]
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        public string SleepText { get; set; }
    }
}
