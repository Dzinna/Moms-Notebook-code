using EnumsNET;
using MomsNotebook.Models.Enums;
using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Tooth
{
    public partial class ToothsPage : ContentPage
    {
        ToothsViewModel ToothsViewModel { get; set; }

        public ToothsPage()
        {
            InitializeComponent();
            ToothsViewModel = new ToothsViewModel();
            BindingContext = ToothsViewModel;
        }

        public ToothsPage(ToothsViewModel toothsViewModel)
        {
            InitializeComponent();
            ToothsViewModel = toothsViewModel;

            ToothsViewModel.JawSelectedItem = new KeyValuePair<int, string>((int)toothsViewModel.Tooth.Jaw, Enums.GetMember<Jaw>(toothsViewModel.Tooth.Jaw.ToString()).Attributes.Get<DescriptionAttribute>().Description);
            ToothsViewModel.JawSideSelectedItem = new KeyValuePair<int, string>((int)toothsViewModel.Tooth.JawSide, Enums.GetMember<JawSide>(toothsViewModel.Tooth.JawSide.ToString()).Attributes.Get<DescriptionAttribute>().Description);
            ToothsViewModel.ToothSelectedItem = new KeyValuePair<int, string>((int)toothsViewModel.Tooth.ToothNumber, Enums.GetMember<ToothNumber>(toothsViewModel.Tooth.ToothNumber.ToString()).Attributes.Get<DescriptionAttribute>().Description);

            BindingContext = ToothsViewModel;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }
        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if (ToothsViewModel.JawSelectedItem.Value == null)
            {
                await DisplayAlert("Dantys.", "Nepasirinkta žandikaulio pusė.", "Rinktis.");
                return;
            }

            if (ToothsViewModel.JawSideSelectedItem.Value == null)
            {
                await DisplayAlert("Dantys.", "Nepasirinkta dantų pusė.", "Rinktis.");
                return;
            }

            if (ToothsViewModel.ToothSelectedItem.Value == null)
            {
                await DisplayAlert("Dantys.", "Nepasirinktas išdygęs dantukas.", "Rinktis.");
                return;
            }

            ToothsViewModel.Tooth.Jaw = (Jaw)ToothsViewModel.JawSelectedItem.Key;
            ToothsViewModel.Tooth.JawSide = (JawSide)ToothsViewModel.JawSideSelectedItem.Key;
            ToothsViewModel.Tooth.ToothNumber = (ToothNumber)ToothsViewModel.ToothSelectedItem.Key;
            ToothsViewModel.Tooth.ActualTime = StartDatePicker.Date.FormatDateTime(CurrentTimePicker.Time);
            ToothsViewModel.Tooth.CombinedToothText = $"{ToothsViewModel.JawSelectedItem.Value} žandikaulis, {ToothsViewModel.JawSideSelectedItem.Value.ToLower()} pusė, {ToothsViewModel.ToothSelectedItem.Value.ToLower()} dantukas";

            MessagingCenter.Send(this, "AddOrUpdateTooth", ToothsViewModel.Tooth);

            await Navigation.PopAsync();
        }
        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteTooth = await DisplayAlert(
                "Ištrinti dantuką.",
                "Ar norite ištrinti šį įrašą ?",
                "Taip",
                "Ne");

            if (deleteTooth)
            {
                MessagingCenter.Send(this, "DeleteTooth", ToothsViewModel.Tooth);
            }

            await Navigation.PopAsync();
        }
    }
}