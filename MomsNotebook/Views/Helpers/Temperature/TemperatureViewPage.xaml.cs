using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Temperature
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TemperatureViewPage : ContentPage
    {
        TemperatureViewModel TemperatureViewModel { get; }

        public TemperatureViewPage()
        {
            InitializeComponent();

            TemperatureViewModel = new TemperatureViewModel();

            BindingContext = TemperatureViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public TemperatureViewPage(TemperatureViewModel temperatureViewModel)
        {
            InitializeComponent();

            TemperatureViewModel = temperatureViewModel;

            BindingContext = TemperatureViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            TemperatureQuantity.Value = TemperatureViewModel.Temperature.Value;

        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            TemperatureViewModel.Temperature.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            TemperatureViewModel.Temperature.Value = TemperatureQuantity.Value;

            MessagingCenter.Send(this, "AddOrUpdateTemperature", TemperatureViewModel.Temperature);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteTemperature = await DisplayAlert(
                "Ištrinti temperatūrą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteTemperature)
            {
                MessagingCenter.Send(this, "DeleteTemperature", TemperatureViewModel.Temperature);
            }                

            await Navigation.PopAsync();
        }
    }
}