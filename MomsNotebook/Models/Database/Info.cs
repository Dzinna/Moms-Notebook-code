using SQLite;

namespace MomsNotebook.Models.Database
{
    public class Info : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Weblink { get; set; }
    }
}
