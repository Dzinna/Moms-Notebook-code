using MomsNotebook.Models.Database;
using MomsNotebook.Models.Enums;
using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChestFeedViewPage : ContentPage
    {
        FeedingPageViewModel FeedingPageViewModel { get; set; }

        public ChestFeedViewPage(FoodType foodType)
        {
            InitializeComponent();

            FeedingPageViewModel = new FeedingPageViewModel
            {
                Feeding = new Feeding
                {
                    FoodType = foodType
                }
            };

            BindingContext = FeedingPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FeedingPageViewModel.GetDataByChestFeedingSide.Execute(null);

            FeedingListView.ItemsSource = FeedingPageViewModel.FeedingCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var feeding = FeedingListView.SelectedItem as Feeding;
            
            if (feeding == null)
                return;

            FeedingPageViewModel.Feeding.MapFeedings(feeding);

            await Navigation.PushAsync(new ChestFeedPage(FeedingPageViewModel));

            // Manually deselect item.
            FeedingListView.SelectedItem = null;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ChestFeedPage());
        }
    }
}