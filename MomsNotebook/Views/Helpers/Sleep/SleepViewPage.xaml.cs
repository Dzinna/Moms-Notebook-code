using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Sleep
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SleepViewPage : ContentPage
    {
        SleepViewModel SleepViewModel { get; }

        public SleepViewPage()
        {
            InitializeComponent();

            SleepViewModel = new SleepViewModel();

            BindingContext = SleepViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            EndTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public SleepViewPage(SleepViewModel sleepViewModel)
        {
            InitializeComponent();

            SleepViewModel = sleepViewModel;

            BindingContext = SleepViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            EndTimePicker.Time = DateTime.Now.TimeOfDay;

            SleepText.Text = sleepViewModel.Sleep.SleepText;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(SleepText.Text == null)
            {
                await DisplayAlert("Miegas", "Neįrašytas miego tekstas.", "Pildyti");
                return;
            }

            SleepViewModel.Sleep.SleepText = SleepText.Text;
            SleepViewModel.Sleep.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            SleepViewModel.Sleep.EndTime = EndDatePicker.Date.FormatDateTime(EndTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateSleep", SleepViewModel.Sleep);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var sleepDelete = await DisplayAlert(
                "Ištrinti miego įrašą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (sleepDelete)
            {
                MessagingCenter.Send(this, "DeleteSleep", SleepViewModel.Sleep);
            }

            await Navigation.PopAsync();
        }
    }
}