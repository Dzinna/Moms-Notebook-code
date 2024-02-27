using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Contacts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using MomsNotebook.Models.Database;

namespace MomsNotebook.ViewModels
{
    public class ContactsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Contacts> Contacts { get; set; } = new ObservableCollection<Contacts>();
        public Contacts Contact { get; set; } = new Contacts();
        public Command PopulateListCommand { get; set; }

        public ContactsPageViewModel()
        {
            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            Contacts.Clear();

            var contacts = await Database.ReadAllContacts();

            contacts = contacts.Where(x => x.ContactType == Contact.ContactType).ToList();

            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<ContactsViewPage, Contacts>(this, "AddContact", async (sender, contact) =>
            {
                if (contact.Key == null)
                {
                    contact.Key = Guid.NewGuid().ToString();
                    await Database.Insert(contact);
                }
                else
                {
                    await Database.Update(contact);
                }
            });

            MessagingCenter.Subscribe<ContactsViewPage, Contacts>(this, "DeleteContact", async (sender, contact) =>
            {
                await Database.Delete(contact);
            });
        }
    }
}
