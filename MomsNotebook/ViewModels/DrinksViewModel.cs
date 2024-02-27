using MomsNotebook.Models.Database;
using System;
using System.Collections.ObjectModel;
using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Drink;
using Xamarin.Forms;

namespace MomsNotebook.ViewModels
{
    public class DrinksViewModel : BaseViewModel
    {
        public ObservableCollection<Drinks> DrinksCollection { get; set; } = new ObservableCollection<Drinks>();
        public Drinks Drink { get; set; } = new Drinks();
        public Command DrinkViewCommand { get; set; }

        public DrinksViewModel()
        {
            DrinkViewCommand = new Command(() => Initialize());
            Title = "Gėrimai";
            InitializeSubscribes();
        }

        async void Initialize()
        {
            DrinksCollection.Clear();

            var drinksData = await Database.ReadAllDrinks();

            foreach (var drink in drinksData)
            {
                DrinksCollection.Add(drink);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<DrinksViewPage, Drinks>(this, "AddOrUpdateDrink", async (sender, drink) =>
            {
                if (drink.Key == null)
                {
                    drink.Key = Guid.NewGuid().ToString();
                    await Database.Insert(drink);
                }
                else
                {
                    await Database.Update(drink);
                }
            });

            MessagingCenter.Subscribe<DrinksViewPage, Drinks>(this, "DeleteDrink", async (sender, drink) =>
            {
                await Database.Delete(drink);
            });
        }
    }
}
