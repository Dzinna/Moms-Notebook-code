using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Flu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlusViewPage : ContentPage
    {
        FlusViewModel FlusViewModel { get; }

        public FlusViewPage()
        {
            InitializeComponent();

            FlusViewModel = new FlusViewModel();

            BindingContext = FlusViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public FlusViewPage(FlusViewModel flusViewModel)
        {
            InitializeComponent();

            FlusViewModel = flusViewModel;

            BindingContext = FlusViewModel;

            CurrentTimePicker.Time = FlusViewModel.Flu.ActualTime.TimeOfDay;

            StartDatePicker.Date = FlusViewModel.Flu.ActualTime.Date;

            FlusText.Text = FlusViewModel.Flu.FluName;
            FluNotes.Text = FlusViewModel.Flu.Notes;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(FlusText.Text))
            {
                await DisplayAlert("Liga", "Neįrašytas ligos pavadinimas.", "Pildyti");
                return;
            }

            FlusViewModel.Flu.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            FlusViewModel.Flu.FluName = FlusText.Text;
            FlusViewModel.Flu.Notes = FluNotes.Text;

            MessagingCenter.Send(this, "AddOrUpdateFlu", FlusViewModel.Flu);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var bathing = await DisplayAlert(
                "Ištrinti įrašą apie ligą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (bathing)
                MessagingCenter.Send(this, "DeleteFlu", FlusViewModel.Flu);

            await Navigation.PopAsync();
        }
    }
}