using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using SleepTable = MomsNotebook.Models.Database.Sleep;

namespace MomsNotebook.Views.Helpers.Sleep
{
    public partial class SleepPage : ContentPage
    {
        public SleepViewModel SleepViewModel { get; set; }
        public SleepPage()
        {
            InitializeComponent();

            SleepViewModel = new SleepViewModel();
            BindingContext = SleepViewModel;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new SleepViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SleepViewModel.PopulateListCommand.Execute(null);

            SleepListView.ItemsSource = SleepViewModel.SleepCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var sleep = SleepListView.SelectedItem as SleepTable;

            if (sleep == null)
            {
                return;
            }

            SleepViewModel.Sleep = sleep;

            await Navigation.PushAsync(new SleepViewPage(SleepViewModel));

            SleepListView.SelectedItem = null;
        }
    }
}