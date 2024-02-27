using MomsNotebook.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using MomsNotebook.Views.Helpers.Drugs;
using MomsNotebook.Models.Database;

namespace MomsNotebook.ViewModels
{
    public class DrugsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Drugs> DrugsObservable { get; set; } = new ObservableCollection<Drugs>();
        public Drugs Drug { get; set; } = new Drugs();
        public Command PopulateListCommand { get; set; }

        public DrugsPageViewModel()
        {
            Title = "Vaistai";
            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            DrugsObservable.Clear();

            var drugs = await Database.ReadAllDrugs();

            foreach (var drug in drugs)
            {
                DrugsObservable.Add(drug);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<DrugsPage, Drugs>(this, "AddOrUpdateDrug", async (sender, drug) =>
            {
                if (drug.Key == null)
                {
                    drug.Key = Guid.NewGuid().ToString();
                    await Database.Insert(drug);
                }
                else
                {
                    await Database.Update(drug);
                }
            });
        }
    }
}
