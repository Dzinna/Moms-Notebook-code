using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Bathing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BathingsViewPage : ContentPage
    {
        BathingViewModel BathingViewModel { get; }

        public BathingsViewPage()
        {
            InitializeComponent();

            BathingViewModel = new BathingViewModel();

            BindingContext = BathingViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public BathingsViewPage(BathingViewModel bathingViewModel)
        {
            InitializeComponent();

            BathingViewModel = bathingViewModel;

            BindingContext = BathingViewModel;

            CurrentTimePicker.Time = BathingViewModel.Bathing.ActualTime.TimeOfDay;

            StartDatePicker.Date = BathingViewModel.Bathing.ActualTime.Date;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(BathingText.Text))
            {
                await DisplayAlert("Maudymas", "Neįrašytas maudymo tekstas.", "Pildyti");
                return;
            }

            BathingViewModel.Bathing.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            BathingViewModel.Bathing.Text = BathingText.Text;

            MessagingCenter.Send(this, "AddOrUpdateBathing", BathingViewModel.Bathing);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteBathing = await DisplayAlert(
                "Ištrinti įrašą apie maudymą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteBathing)
            {
                MessagingCenter.Send(this, "DeleteBathing", BathingViewModel.Bathing);
            }

            await Navigation.PopAsync();
        }
    }
}