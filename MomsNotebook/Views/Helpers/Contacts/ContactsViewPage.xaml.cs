using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsViewPage : ContentPage
    {
        ContactsPageViewModel ContactsViewmodel { get; set; }
        public ContactsViewPage()
        {
            InitializeComponent();

            ContactsViewmodel = new ContactsPageViewModel();
            BindingContext = ContactsViewmodel;           
        }

        public ContactsViewPage(ContactsPageViewModel contactsPageViewModel)
        {
            InitializeComponent();

            ContactsViewmodel = contactsPageViewModel;

            BindingContext = ContactsViewmodel;
        }

        async void Cancel_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PopAsync();
        }

        async void Save_Clicked(object sender, EventArgs eventArgs)
        {
            if(string.IsNullOrEmpty(ContactName.Text))
            {
                await DisplayAlert(
                "Laukai neužpildyti !",
                "Jūs privalote užpildyti 'Kontakto pavadinimą'",
                "Pildyti");
                return;
            }

            MessagingCenter.Send(this, "AddContact", ContactsViewmodel.Contact);

            await Navigation.PopAsync();
        }

        async void Delete_Clicked(object sender, EventArgs eventArgs)
        {
            var deleteContact = await DisplayAlert(
                "Ištrinti kontaktą.",
                "Ar norite ištrinti pasirinktą kontaktą ?",
                "Taip",
                "Ne");

            if (deleteContact)
            {
                MessagingCenter.Send(this, "DeleteContact", ContactsViewmodel.Contact);
            }

            await Navigation.PopAsync();
        }
    }
}