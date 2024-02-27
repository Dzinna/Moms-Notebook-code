using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Health;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class HealthViewModel : BaseViewModel
    {
        public ObservableCollection<Health> HealthCollection { get; set; } = new ObservableCollection<Health>();
        public Health Health { get; set; } = new Health();
        public Command PopulateListCommand { get; set; }

        public HealthViewModel()
        {

            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            HealthCollection.Clear();

            var healthData = await Database.ReadAllHealth();

            foreach (var health in healthData)
            {
                HealthCollection.Add(health);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<HealthViewPage, Health>(this, "AddOrUpdateHealth", async (sender, health) =>
            {
                if (health.Key == null)
                {
                    health.Key = Guid.NewGuid().ToString();
                    await Database.Insert(health);
                }
                else
                {
                    await Database.Update(health);
                }
            });

            MessagingCenter.Subscribe<HealthViewPage, Health>(this, "DeleteHealth", async (sender, health) =>
            {
                await Database.Delete(health);
            });
        }
    }
}
