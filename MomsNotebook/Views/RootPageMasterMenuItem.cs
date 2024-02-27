using System;

namespace MomsNotebook.Views
{
    public class RootPageMasterMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public Helpers.Move.MovesViewPage MovesViewPage
        {
            get => default;
            set
            {
            }
        }
    }
}