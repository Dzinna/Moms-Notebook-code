using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MomsNotebook.ViewModels;

namespace MomsNotebook.Views.Helpers.Drugs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrugsPage : ContentPage
    {
        DrugsPageViewModel DrugsPageViewModel;

        public DrugsPage()
        {
            InitializeComponent();

            DrugsPageViewModel = new DrugsPageViewModel();
            BindingContext = DrugsPageViewModel;

            CurrentTimePicker.Time = DateTime.UtcNow.TimeOfDay;
        }

        public DrugsPage(DrugsPageViewModel drugsPageViewModel)
        {
            InitializeComponent();

            DrugsPageViewModel = drugsPageViewModel;

            BindingContext = DrugsPageViewModel;

            CurrentTimePicker.Time = DateTime.UtcNow.TimeOfDay;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopModalAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            MessagingCenter.Send(this, "AddOrUpdateDrug", DrugsPageViewModel.Drug);

            await Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteDrug = await DisplayAlert(
                "Delete option",
                "Do you want to delete Contacts ?",
                "Yes",
                "No");

            if (deleteDrug)
            {
                MessagingCenter.Send(this, "DeleteDrug", DrugsPageViewModel.Drug);
            }

            await Navigation.PopModalAsync();
        }
    }
}