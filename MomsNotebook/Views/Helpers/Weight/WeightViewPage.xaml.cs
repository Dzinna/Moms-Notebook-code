using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Weight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightViewPage : ContentPage
    {        
        BabyWeightViewModel BabyWeightViewModel { get; }

        public WeightViewPage()
        {
            InitializeComponent();

            BabyWeightViewModel = new BabyWeightViewModel();

            BindingContext = BabyWeightViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;            
        }

        public WeightViewPage(BabyWeightViewModel babyWeightViewModel)
        {
            InitializeComponent();

            BabyWeightViewModel = babyWeightViewModel;

            BindingContext = BabyWeightViewModel; 
            
            CurrentTimePicker.Time = BabyWeightViewModel.BabyWeight.ActualTime.TimeOfDay;
            StartDatePicker.Date = BabyWeightViewModel.BabyWeight.ActualTime.Date;
            WeightQuantity.Value = BabyWeightViewModel.BabyWeight.Weight;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            var time = CurrentTimePicker.Time;
            var date = StartDatePicker.Date;
            var dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);

            BabyWeightViewModel.BabyWeight.Weight = WeightQuantity.Value;
            BabyWeightViewModel.BabyWeight.ActualTime = dateTime;

            MessagingCenter.Send(this, "AddOrUpdateBabyWeight", BabyWeightViewModel.BabyWeight);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var svoris = await DisplayAlert(
                "Ištrinti svorį.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (svoris)
                MessagingCenter.Send(this, "DeleteBabyWeight", BabyWeightViewModel.BabyWeight);

            await Navigation.PopAsync();
        }
    }
}