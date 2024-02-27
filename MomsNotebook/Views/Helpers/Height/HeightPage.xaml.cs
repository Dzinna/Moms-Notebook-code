using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Height
{
    public partial class HeightPage : ContentPage
    {
        public BabyHeightViewModel BabyHeightViewModel { get; set; }

        public HeightPage()
        {
            InitializeComponent();

            BabyHeightViewModel = new BabyHeightViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new HeightViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BabyHeightViewModel.BabyHeightViewCommand.Execute(null);

            BabyHeightListView.ItemsSource = BabyHeightViewModel.BabyHeightCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var babyHeight = BabyHeightListView.SelectedItem as Heights;

            if (babyHeight == null)
                return;

            BabyHeightViewModel.BabyHeight = babyHeight;

            await Navigation.PushAsync(new HeightViewPage(BabyHeightViewModel));

            BabyHeightListView.SelectedItem = null;
        }
    }
}