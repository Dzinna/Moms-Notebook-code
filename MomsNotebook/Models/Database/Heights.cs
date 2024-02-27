using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Heights : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public int Height { get; set; }
    }
}
