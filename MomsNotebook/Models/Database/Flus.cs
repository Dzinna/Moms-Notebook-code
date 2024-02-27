using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Flus : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public string FluName { get; set; }
        public string Notes { get; set; }
    }
}
