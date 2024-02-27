using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Bathings : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public string Text { get; set; }
    }
}
