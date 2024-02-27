using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Drink
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrinksViewPage : ContentPage
    {
        DrinksViewModel DrinksViewModel { get; }

        public DrinksViewPage()
        {
            InitializeComponent();

            DrinksViewModel = new DrinksViewModel();

            BindingContext = DrinksViewModel;

            CurrentTimePicker.Time = DateTime.Now.TimeOfDay;
        }

        public DrinksViewPage(DrinksViewModel drinksViewModel)
        {
            InitializeComponent();

            DrinksViewModel = drinksViewModel;

            BindingContext = DrinksViewModel;

            CurrentTimePicker.Time = DrinksViewModel.Drink.ActualTime.TimeOfDay;

            StartDatePicker.Date = DrinksViewModel.Drink.ActualTime.Date;

            DrinkQuantity.Value = DrinksViewModel.Drink.Quantity;

            DrinksText.Text = drinksViewModel.Drink.DrinkText;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if (string.IsNullOrEmpty(DrinksText.Text))
            {
                await DisplayAlert("Vystymas", "Neįrašytas vystymo veiksmas.", "Pildyti");
                return;
            }

            DrinksViewModel.Drink.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            DrinksViewModel.Drink.DrinkText = DrinksText.Text;
            DrinksViewModel.Drink.Quantity = DrinkQuantity.Value;

            MessagingCenter.Send(this, "AddOrUpdateDrink", DrinksViewModel.Drink);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteDrink = await DisplayAlert(
                "Ištrinti įrašą apie gėrimą.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteDrink)
            {
                MessagingCenter.Send(this, "DeleteDrink", DrinksViewModel.Drink);
            }

            await Navigation.PopAsync();
        }
    }
}