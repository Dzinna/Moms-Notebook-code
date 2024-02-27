using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Weight
{
    public partial class WeightPage : ContentPage
    {
        public BabyWeightViewModel BabyWeightViewModel { get; set; }

        public WeightPage()
        {
            InitializeComponent();

            BabyWeightViewModel = new BabyWeightViewModel();
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new WeightViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BabyWeightViewModel.BabyWeightViewCommand.Execute(null);

            BabyWeightListView.ItemsSource = BabyWeightViewModel.BabyWeightCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var babyWeight = BabyWeightListView.SelectedItem as Weights;

            if (babyWeight == null)
                return;

            BabyWeightViewModel.BabyWeight = babyWeight;

            await Navigation.PushAsync(new WeightViewPage(BabyWeightViewModel));

            BabyWeightListView.SelectedItem = null;
        }
    }
}