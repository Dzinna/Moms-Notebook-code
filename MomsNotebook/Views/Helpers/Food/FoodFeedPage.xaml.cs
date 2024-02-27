using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MomsNotebook.Utils;
using MomsNotebook.ViewModels;

namespace MomsNotebook.Views.Helpers.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodFeedPage : ContentPage
    {        
        FeedingPageViewModel FeedingPageViewModel { get; }

        public FoodFeedPage(FeedingPageViewModel feedingPageViewModel)
        {
            InitializeComponent();

            FeedingPageViewModel = feedingPageViewModel;

            BindingContext = FeedingPageViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(FoodType.Text == null)
            {
                await DisplayAlert("Maitinimas", "Neįrašytas maisto tipas.", "Pildyti");
                return;
            }
           
            FeedingPageViewModel.Feeding.FoodDescription = FoodType.Text;
            FeedingPageViewModel.Feeding.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            FeedingPageViewModel.Feeding.Quantity = (int)FoodTypeQuantity.Value;

            MessagingCenter.Send(this, "AddOrUpdateFoodType", FeedingPageViewModel.Feeding);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var maitinimas = await DisplayAlert(
                "Ištrinti maitinimą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (maitinimas)
                MessagingCenter.Send(this, "DeleteFoodType", FeedingPageViewModel.Feeding);

            await Navigation.PopAsync();
        }
    }
}