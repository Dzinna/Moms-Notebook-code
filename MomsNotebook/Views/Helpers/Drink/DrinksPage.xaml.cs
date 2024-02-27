using System;
using Xamarin.Forms;
using MomsNotebook.ViewModels;
using MomsNotebook.Models.Database;

namespace MomsNotebook.Views.Helpers.Drink
{
    public partial class DrinksPage : ContentPage
    {
        public DrinksViewModel DrinksViewModel { get; set; }

        public DrinksPage()
        {
            InitializeComponent();

            DrinksViewModel = new DrinksViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new DrinksViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DrinksViewModel.DrinkViewCommand.Execute(null);

            DrinksListView.ItemsSource = DrinksViewModel.DrinksCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var drink = DrinksListView.SelectedItem as Drinks;

            if (drink == null)
                return;

            DrinksViewModel.Drink = drink;

            await Navigation.PushAsync(new DrinksViewPage(DrinksViewModel));

            DrinksListView.SelectedItem = null;
        }
    }
}