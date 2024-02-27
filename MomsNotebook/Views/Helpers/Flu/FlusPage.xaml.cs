using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Flu
{
    public partial class FlusPage : ContentPage
    {
        public FlusViewModel FlusViewModel { get; set; }

        public FlusPage()
        {
            InitializeComponent();

            FlusViewModel = new FlusViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new FlusViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FlusViewModel.FluViewCommand.Execute(null);

            FlusListView.ItemsSource = FlusViewModel.FlusCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var flu = FlusListView.SelectedItem as Flus;

            if (flu == null)
                return;

            FlusViewModel.Flu = flu;

            await Navigation.PushAsync(new FlusViewPage(FlusViewModel));

            FlusListView.SelectedItem = null;
        }
    }
}