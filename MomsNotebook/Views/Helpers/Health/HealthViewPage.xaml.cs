using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Health
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HealthViewPage : ContentPage
    {        
        HealthViewModel HealthViewModel { get; }

        public HealthViewPage()
        {
            InitializeComponent();

            HealthViewModel = new HealthViewModel();

            BindingContext = HealthViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public HealthViewPage(HealthViewModel healthViewModel)
        {
            InitializeComponent();

            HealthViewModel = healthViewModel;

            BindingContext = HealthViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            HealthText.Text = HealthViewModel.Health.Text;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(HealthText.Text == null)
            {
                await DisplayAlert("Savijauta", "Neįrašyta kūdikio savijauta.", "Pildyti");
                return;
            }

            HealthViewModel.Health.Text = HealthText.Text;
            HealthViewModel.Health.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateHealth", HealthViewModel.Health);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteHealth = await DisplayAlert(
                "Ištrinti maitinimą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteHealth)
            {
                MessagingCenter.Send(this, "DeleteHealth", HealthViewModel.Health);
            }

            await Navigation.PopAsync();
        }
    }
}