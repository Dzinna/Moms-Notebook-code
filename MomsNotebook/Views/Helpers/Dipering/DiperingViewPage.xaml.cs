using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Dipering
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiperingViewPage : ContentPage
    {        
        DiperingViewModel DiperingViewModel { get; }

        public DiperingViewPage()
        {
            InitializeComponent();

            DiperingViewModel = new DiperingViewModel();

            BindingContext = DiperingViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public DiperingViewPage(DiperingViewModel diperingViewModel)
        {
            InitializeComponent();

            DiperingViewModel = diperingViewModel;

            BindingContext = DiperingViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(string.IsNullOrEmpty(DiperText.Text))
            {
                await DisplayAlert("Vystymas", "Neįrašytas vystymo veiksmas.", "Pildyti");
                return;
            }

            DiperingViewModel.Diper.Text = DiperText.Text;
            DiperingViewModel.Diper.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateDiper", DiperingViewModel.Diper);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteDiper = await DisplayAlert(
                "Ištrinti vystymą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteDiper)
            {
                MessagingCenter.Send(this, "DeleteDiper", DiperingViewModel.Diper);
            }

            await Navigation.PopAsync();
        }
    }
}