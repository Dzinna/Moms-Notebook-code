using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Move;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class MovesViewModel : BaseViewModel
    {
        public ObservableCollection<Moves> MovesCollection { get; set; } = new ObservableCollection<Moves>();
        public Moves Move { get; set; } = new Moves();
        public Command MoveCommand { get; set; }

        public MovesViewModel()
        {
            MoveCommand = new Command(() => Initialize());

            InitializeSubscribes();
        }

        async void Initialize()
        {
            MovesCollection.Clear();

            var movesData = await Database.ReadAllMoves();

            foreach (var move in movesData)
            {
                MovesCollection.Add(move);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<MovesViewPage, Moves>(this, "AddOrUpdateMoves", async (sender, move) =>
            {
                if (move.Key == null)
                {
                    move.Key = Guid.NewGuid().ToString();
                    await Database.Insert(move);
                }
                else
                {
                    await Database.Update(move);
                }
            });

            MessagingCenter.Subscribe<MovesViewPage, Moves>(this, "DeleteMove", async (sender, move) =>
            {
                await Database.Delete(move);
            });
        }
    }
}
