using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Sleep;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class SleepViewModel : BaseViewModel
    {
        public ObservableCollection<Sleep> SleepCollection { get; set; } = new ObservableCollection<Sleep>();
        public Sleep Sleep { get; set; } = new Sleep();
        public Command PopulateListCommand { get; set; }

        public SleepViewModel()
        {
            Title = "Miegas";
            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            SleepCollection.Clear();

            var sleepData = await Database.ReadAllSleep();

            foreach (var sleep in sleepData)
            {
                SleepCollection.Add(sleep);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<SleepViewPage, Sleep>(this, "AddOrUpdateSleep", async (sender, sleep) =>
            {
                if (sleep.Key == null)
                {
                    sleep.Key = Guid.NewGuid().ToString();
                    await Database.Insert(sleep);
                }
                else
                {
                    await Database.Update(sleep);
                }
            });

            MessagingCenter.Subscribe<SleepViewPage, Sleep>(this, "DeleteSleep", async (sender, sleep) =>
            {
                await Database.Delete(sleep);
            });
        }
    }
}
