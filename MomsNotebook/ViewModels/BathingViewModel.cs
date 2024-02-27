using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Bathing;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class BathingViewModel : BaseViewModel
    {
        public ObservableCollection<Bathings> BathingsCollection { get; set; } = new ObservableCollection<Bathings>();
        public Bathings Bathing { get; set; } = new Bathings();
        public Command BathingViewCommand { get; set; }

        public BathingViewModel()
        {
            BathingViewCommand = new Command(() => Initialize());

            InitializeSubscribes();
        }

        async void Initialize()
        {
            BathingsCollection.Clear();

            var bathingData = await Database.ReadAllBathings();

            foreach (var bathing in bathingData)
            {
                BathingsCollection.Add(bathing);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<BathingsViewPage, Bathings>(this, "AddOrUpdateBathing", async (sender, bathing) =>
            {
                if (bathing.Key == null)
                {
                    bathing.Key = Guid.NewGuid().ToString();
                    await Database.Insert(bathing);
                }
                else
                {
                    await Database.Update(bathing);
                }
            });

            MessagingCenter.Subscribe<BathingsViewPage, Bathings>(this, "DeleteBathing", async (sender, bathing) =>
            {
                await Database.Delete(bathing);
            });
        }
    }
}
