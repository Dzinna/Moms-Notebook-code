using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Weight;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class BabyWeightViewModel : BaseViewModel
    {
        public ObservableCollection<Weights> BabyWeightCollection { get; set; } = new ObservableCollection<Weights>();
        public Weights BabyWeight { get; set; } = new Weights();
        public Command BabyWeightViewCommand { get; set; }

        public BabyWeightViewModel()
        {
            BabyWeightViewCommand = new Command(() => Initialize());
            Title = "Svoris";
            InitializeSubscribes();
        }

        async void Initialize()
        {
            BabyWeightCollection.Clear();

            var babyHeightData = await Database.ReadAllBabyWeights();

            foreach (var babyWeight in babyHeightData)
            {
                BabyWeightCollection.Add(babyWeight);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<WeightViewPage, Weights>(this, "AddOrUpdateBabyWeight", async (sender, weight) =>
            {
                if (weight.Key == null)
                {
                    weight.Key = Guid.NewGuid().ToString();
                    await Database.Insert(weight);
                }
                else
                {
                    await Database.Update(weight);
                }
            });

            MessagingCenter.Subscribe<WeightViewPage, Weights>(this, "DeleteBabyWeight", async (sender, weight) =>
            {
                await Database.Delete(weight);
            });
        }
    }
}
