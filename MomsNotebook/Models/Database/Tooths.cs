using SQLite;
using System;
using MomsNotebook.Models.Enums;

namespace MomsNotebook.Models.Database
{
    public class Tooths : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        public Jaw Jaw { get; set; }
        public JawSide JawSide { get; set; }
        public ToothNumber ToothNumber { get; set; }
        public string CombinedToothText { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
    }
}
