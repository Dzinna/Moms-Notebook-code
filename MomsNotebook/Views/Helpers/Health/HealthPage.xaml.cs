using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using HealthTable = MomsNotebook.Models.Database.Health;

namespace MomsNotebook.Views.Helpers.Health
{
    public partial class HealthPage : ContentPage
    {
        public HealthViewModel HealthViewModel { get; set; }
        public HealthPage()
        {
            InitializeComponent();

            HealthViewModel = new HealthViewModel();
            BindingContext = HealthViewModel;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new HealthViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            HealthViewModel.PopulateListCommand.Execute(null);

            HealthListView.ItemsSource = HealthViewModel.HealthCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var health = HealthListView.SelectedItem as HealthTable;

            if (health == null)
            {
                return;
            }

            HealthViewModel.Health = health;

            await Navigation.PushAsync(new HealthViewPage(HealthViewModel));

            HealthListView.SelectedItem = null;
        }
    }
}