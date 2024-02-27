using SQLite;
using System;

namespace MomsNotebook.Models.Database
{
    public class Notes : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public string NoteText { get; set; }
    }
}
