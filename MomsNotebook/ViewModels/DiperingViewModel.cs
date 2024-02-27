using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Dipering;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class DiperingViewModel : BaseViewModel
    {
        public ObservableCollection<Dipers> DiperingsCollection { get; set; } = new ObservableCollection<Dipers>();
        public Dipers Diper { get; set; } = new Dipers();
        public Command PopulateListCommand { get; set; }

        public DiperingViewModel()
        {

            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            DiperingsCollection.Clear();

            var diperings = await Database.ReadAllDiperings();

            foreach (var diper in diperings)
            {
                DiperingsCollection.Add(diper);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<DiperingViewPage, Dipers>(this, "AddOrUpdateDiper", async (sender, diper) =>
            {
                if (diper.Key == null)
                {
                    diper.Key = Guid.NewGuid().ToString();
                    await Database.Insert(diper);
                }
                else
                {
                    await Database.Update(diper);
                }
            });

            MessagingCenter.Subscribe<DiperingViewPage, Dipers>(this, "DeleteDiper", async (sender, diper) =>
            {
                await Database.Delete(diper);
            });
        }
    }
}
