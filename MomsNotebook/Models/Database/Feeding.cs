using SQLite;
using System;
using MomsNotebook.Models.Enums;

namespace MomsNotebook.Models.Database
{
    public class Feeding : ITable
    {
        [PrimaryKey, Unique]
        public string Key { get; set; }
        [NotNull]
        public DateTime ActualTime { get; set; } = DateTime.UtcNow;
        public FoodType FoodType { get; set; }
        public string FoodDescription { get; set; }
        public int Quantity { get; set; }
    }
}
