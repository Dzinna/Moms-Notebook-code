using MomsNotebook.Models.Enums;
using SQLite;

namespace MomsNotebook.Models.Database
{
    public class Contacts : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [Indexed]
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string Telephone { get; set; }
        public string Mailbox { get; set; }
        public ContactType ContactType { get; set; }
    }
}
