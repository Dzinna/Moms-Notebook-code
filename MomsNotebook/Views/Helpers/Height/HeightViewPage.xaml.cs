using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Height
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeightViewPage : ContentPage
    {        
        BabyHeightViewModel BabyHeightViewModel { get; }

        public HeightViewPage()
        {
            InitializeComponent();

            BabyHeightViewModel = new BabyHeightViewModel();

            BindingContext = BabyHeightViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public HeightViewPage(BabyHeightViewModel babyHeightViewModel)
        {
            InitializeComponent();

            BabyHeightViewModel = babyHeightViewModel;

            BindingContext = BabyHeightViewModel;

            CurrentTimePicker.Time = BabyHeightViewModel.BabyHeight.ActualTime.TimeOfDay;

            StartDatePicker.Date = BabyHeightViewModel.BabyHeight.ActualTime.Date;

            BabyHeightStepper.Value = BabyHeightViewModel.BabyHeight.Height;
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

            BabyHeightViewModel.BabyHeight.ActualTime = dateTime;
            BabyHeightViewModel.BabyHeight.Height = (int)BabyHeightStepper.Value;

            MessagingCenter.Send(this, "AddOrUpdateBabyHeight", BabyHeightViewModel.BabyHeight);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var height = await DisplayAlert(
                "Ištrinti įrašą apie ūgį.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if(height)
                MessagingCenter.Send(this, "DeleteBabyHeight", BabyHeightViewModel.BabyHeight);

            await Navigation.PopAsync();
        }
    }
}