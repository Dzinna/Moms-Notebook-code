using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Move
{
    public partial class MovesPage : ContentPage
    {
        public MovesViewModel MovesViewModel { get; set; }

        public MovesPage()
        {
            InitializeComponent();

            MovesViewModel = new MovesViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new MovesViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MovesViewModel.MoveCommand.Execute(null);

            MovesViewList.ItemsSource = MovesViewModel.MovesCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var move = MovesViewList.SelectedItem as Moves;

            if (move == null)
            {
                return;
            }

            MovesViewModel.Move = move;

            await Navigation.PushAsync(new MovesViewPage(MovesViewModel));

            MovesViewList.SelectedItem = null;
        }
    }
}