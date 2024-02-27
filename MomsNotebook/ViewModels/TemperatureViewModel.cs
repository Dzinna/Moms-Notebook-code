using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Temperature;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class TemperatureViewModel : BaseViewModel
    {
        public ObservableCollection<Temperature> TemperatureCollection { get; set; } = new ObservableCollection<Temperature>();
        public Temperature Temperature { get; set; } = new Temperature();
        public Command PopulateListCommand { get; set; }

        public TemperatureViewModel()
        {
            PopulateListCommand = new Command(async () => await InitializeContactTitlesList());

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList()
        {
            TemperatureCollection.Clear();

            var temperatures = await Database.ReadAllTemperature();

            foreach (var temperature in temperatures)
            {
                TemperatureCollection.Add(temperature);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<TemperatureViewPage, Temperature>(this, "AddOrUpdateTemperature", async (sender, temperature) =>
            {
                if (temperature.Key == null)
                {
                    temperature.Key = Guid.NewGuid().ToString();
                    await Database.Insert(temperature);
                }
                else
                {
                    await Database.Update(temperature);
                }
            });

            MessagingCenter.Subscribe<TemperatureViewPage, Temperature>(this, "DeleteTemperature", async (sender, temperature) =>
            {
                await Database.Delete(temperature);
            });
        }
    }
}
