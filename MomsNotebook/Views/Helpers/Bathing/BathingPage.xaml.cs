using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Bathing
{
    public partial class BathingPage : ContentPage
    {
        public BathingViewModel BathingViewModel { get; set; }

        public BathingPage()
        {
            InitializeComponent();

            BathingViewModel = new BathingViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new BathingsViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BathingViewModel.BathingViewCommand.Execute(null);

            BathingListView.ItemsSource = BathingViewModel.BathingsCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var bathing = BathingListView.SelectedItem as Bathings;

            if (bathing == null)
            {
                return;
            }

            BathingViewModel.Bathing = bathing;

            await Navigation.PushAsync(new BathingsViewPage(BathingViewModel));

            BathingListView.SelectedItem = null;
        }
    }
}