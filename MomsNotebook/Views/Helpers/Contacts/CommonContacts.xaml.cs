using MomsNotebook.Models.Enums;
using MomsNotebook.Utils;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using ContactsTable = MomsNotebook.Models.Database.Contacts;

namespace MomsNotebook.Views.Helpers.Contacts
{
    public partial class CommonContacts : ContentPage
    {
        ContactsPageViewModel ContactsPageViewModel { get; set; }

        public CommonContacts()
        {
            InitializeComponent();
            ContactsPageViewModel = new ContactsPageViewModel
            {
                Contact = new ContactsTable
                {
                    ContactType = ContactType.Common
                }
            };

            BindingContext = ContactsPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ContactsPageViewModel.PopulateListCommand.Execute(null);

            ContactListView.ItemsSource = ContactsPageViewModel.Contacts;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var contact = ContactListView.SelectedItem as ContactsTable;
            if (contact == null)
                return;

            ContactsPageViewModel.Contact.MapContacts(contact);

            await Navigation.PushAsync(new ContactsViewPage(ContactsPageViewModel));

            ContactListView.SelectedItem = null;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ContactsViewPage(new ContactsPageViewModel
            {
                Contact = new ContactsTable
                {
                    ContactType = ContactType.Common
                },
                Title = "Bendrinis"
            }));
        }
    }
}