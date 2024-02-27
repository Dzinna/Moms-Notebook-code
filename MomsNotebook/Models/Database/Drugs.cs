using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Drugs : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public string DrugName { get; set; }
        public int DrugSize { get; set; }
    }
}
