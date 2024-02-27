using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Marker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkerPage : ContentPage
    {
        MarkersViewModel MarkersViewModel { get; }
        public MarkerPage()
        {
            InitializeComponent();

            MarkersViewModel = new MarkersViewModel();

            StartDatePicker.Date = DateTime.Now.Date;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (MarkerText.Text == null)
            {
                await DisplayAlert("Įvykis", "Neįrašėte įvykio.", "Pildyti");
                return;
            }

            MarkersViewModel.Marker.Text = MarkerText.Text;
            MarkersViewModel.Marker.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddMarker", MarkersViewModel.Marker);

            await Navigation.PopAsync();
        }
    }
}