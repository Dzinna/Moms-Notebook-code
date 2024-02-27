using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Flu;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class FlusViewModel : BaseViewModel
    {
        public ObservableCollection<Flus> FlusCollection { get; set; } = new ObservableCollection<Flus>();
        public Flus Flu { get; set; } = new Flus();
        public Command FluViewCommand { get; set; }

        public FlusViewModel()
        {
            FluViewCommand = new Command(() => Initialize());
            Title = "Ligos";
            InitializeSubscribes();
        }

        async void Initialize()
        {
            FlusCollection.Clear();

            var flusData = await Database.ReadAllFlus();

            foreach (var flu in flusData)
            {
                FlusCollection.Add(flu);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<FlusViewPage, Flus>(this, "AddOrUpdateFlu", async (sender, flu) =>
            {
                if (flu.Key == null)
                {
                    flu.Key = Guid.NewGuid().ToString();
                    await Database.Insert(flu);
                }
                else
                {
                    await Database.Update(flu);
                }
            });

            MessagingCenter.Subscribe<FlusViewPage, Flus>(this, "DeleteFlu", async (sender, flu) =>
            {
                await Database.Delete(flu);
            });
        }
    }
}
