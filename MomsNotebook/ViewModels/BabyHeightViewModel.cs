using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Height;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class BabyHeightViewModel : BaseViewModel
    {
        public ObservableCollection<Heights> BabyHeightCollection { get; set; } = new ObservableCollection<Heights>();
        public Heights BabyHeight { get; set; } = new Heights();
        public Command BabyHeightViewCommand { get; set; }

        public BabyHeightViewModel()
        {
            BabyHeightViewCommand = new Command(() => Initialize());
            Title = "Ūgis";
            InitializeSubscribes();
        }

        async void Initialize()
        {
            BabyHeightCollection.Clear();

            var babyHeightData = await Database.ReadAllBabyHeights();

            foreach (var babyHeight in babyHeightData)
            {
                BabyHeightCollection.Add(babyHeight);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<HeightViewPage, Heights>(this, "AddOrUpdateBabyHeight", async (sender, height) =>
            {
                if (height.Key == null)
                {
                    height.Key = Guid.NewGuid().ToString();
                    await Database.Insert(height);
                }
                else
                {
                    await Database.Update(height);
                }
            });

            MessagingCenter.Subscribe<HeightViewPage, Heights>(this, "DeleteBabyHeight", async (sender, height) =>
            {
                await Database.Delete(height);
            });
        }
    }
}
