using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MomsNotebook.Models.Enums;
using MomsNotebook.Utils;
using MomsNotebook.ViewModels;

namespace MomsNotebook.Views.Helpers.Food
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChestFeedPage : ContentPage
    {        
        FeedingPageViewModel FeedingPageViewModel { get; }

        public ChestFeedPage()
        {
            InitializeComponent();

            FeedingPageViewModel = new FeedingPageViewModel();

            FeedingPageViewModel.LoadFoodTypes();

            BindingContext = FeedingPageViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public ChestFeedPage(FeedingPageViewModel feedingPageViewModel)
        {
            InitializeComponent();

            FeedingPageViewModel = feedingPageViewModel;

            FeedingPageViewModel.LoadFoodTypes();

            BindingContext = FeedingPageViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(FeedingPageViewModel.SelectedItem.Value == null)
            {
                await DisplayAlert("Maitinimas krūtine.", "Neįrašyta krūtinės pusė.", "Pildyti");
                return;
            }

            FeedingPageViewModel.Feeding.FoodDescription = FeedingPageViewModel.SelectedItem.Value;
            FeedingPageViewModel.Feeding.FoodType = FoodType.Chest;
            FeedingPageViewModel.Feeding.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateFeeding", FeedingPageViewModel.Feeding);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteDrug = await DisplayAlert(
                "Ištrinti maitinimą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteDrug)
                MessagingCenter.Send(this, "DeleteFeeding", FeedingPageViewModel.Feeding);

            await Navigation.PopAsync();
        }
    }
}