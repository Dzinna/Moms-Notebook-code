using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Move
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovesViewPage : ContentPage
    {        
        MovesViewModel MovesViewModel { get; }

        public MovesViewPage()
        {
            InitializeComponent();

            MovesViewModel = new MovesViewModel();

            BindingContext = MovesViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public MovesViewPage(MovesViewModel movesViewModel)
        {
            InitializeComponent();

            MovesViewModel = movesViewModel;

            BindingContext = MovesViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;

            MovesType.Text = MovesViewModel.Move.Text;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(MovesType.Text == null)
            {
                await DisplayAlert("Judesys.", "Neįrašytas judesio tekstas.", "Pildyti");
                return;
            }

            MovesViewModel.Move.Text = MovesType.Text;
            MovesViewModel.Move.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);

            MessagingCenter.Send(this, "AddOrUpdateMoves", MovesViewModel.Move);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var move = await DisplayAlert(
                "Ištrinti judesį.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (move)
                MessagingCenter.Send(this, "DeleteMove", MovesViewModel.Move);

            await Navigation.PopAsync();
        }
    }
}