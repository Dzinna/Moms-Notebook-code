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
    public partial class FoodViewPage : ContentPage
    {
        FeedingPageViewModel FeedingPageViewModel { get; set; }

        public FoodViewPage(FoodType foodType)
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

            FeedingPageViewModel.GetDataByFoodTypes.Execute(null);

            FoodListView.ItemsSource = FeedingPageViewModel.FeedingCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var feeding = FoodListView.SelectedItem as Feeding;

            if (feeding == null)
                return;

            FeedingPageViewModel.Feeding.MapFeedings(feeding);

            await Navigation.PushAsync(new FoodFeedPage(FeedingPageViewModel));

            // Manually deselect item.
            FoodListView.SelectedItem = null;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new FoodFeedPage(new FeedingPageViewModel
            {
                Feeding = new Feeding
                {
                    FoodType = FeedingPageViewModel.Feeding.FoodType
                }
            }));
        }
    }
}