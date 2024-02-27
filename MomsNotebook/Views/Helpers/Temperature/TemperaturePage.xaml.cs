using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using TemperatureTable = MomsNotebook.Models.Database.Temperature;

namespace MomsNotebook.Views.Helpers.Temperature
{
    public partial class TemperaturePage : ContentPage
    {
        public TemperatureViewModel TemperatureViewModel { get; set; }
        public TemperaturePage()
        {
            InitializeComponent();

            TemperatureViewModel = new TemperatureViewModel();
            BindingContext = TemperatureViewModel;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new TemperatureViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TemperatureViewModel.PopulateListCommand.Execute(null);

            TemperatureListView.ItemsSource = TemperatureViewModel.TemperatureCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var temperature = TemperatureListView.SelectedItem as TemperatureTable;

            if (temperature == null)
            {
                return;
            }

            TemperatureViewModel.Temperature = temperature;

            await Navigation.PushAsync(new TemperatureViewPage(TemperatureViewModel));

            TemperatureListView.SelectedItem = null;
        }
    }
}