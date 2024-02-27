using MomsNotebook.Services;
using MomsNotebook.Views.Helpers.Food;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using EnumsNET;
using System.ComponentModel;
using MomsNotebook.Models.Enums;
using MomsNotebook.Models.Database;

namespace MomsNotebook.ViewModels
{
    public class FeedingPageViewModel : BaseViewModel
    {
        public ObservableCollection<Feeding> FeedingCollection { get; set; } = new ObservableCollection<Feeding>();
        public Feeding Feeding { get; set; } = new Feeding();
        public List<KeyValuePair<int, string>> FoodTypeList { get; set; }
        public Command GetDataByChestFeedingSide { get; }
        public Command GetDataByFoodTypes { get; }

        private KeyValuePair<int, string> _selectedItem;
        public KeyValuePair<int, string> SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public FeedingPageViewModel()
        {
            GetDataByChestFeedingSide = new Command(async () => await InitializeContactTitlesList(true));

            GetDataByFoodTypes = new Command(async () => await InitializeContactTitlesList(false));

            InitializeSubscribes();
        }

        async Task InitializeContactTitlesList(bool breastEatingSide)
        {
            FeedingCollection.Clear();

            var feedings = await Database.ReadAllFeedings();

            if (breastEatingSide)
            {
                Title = FoodType.Chest.AsString(EnumFormat.Description);
                feedings = feedings.Where(x => x.FoodType == FoodType.Chest).ToList();
            }
            else
            {
                Title = Feeding.FoodType.AsString(EnumFormat.Description);
                feedings = feedings.Where(x => x.FoodType != FoodType.Chest && x.FoodType == Feeding.FoodType).ToList();
            }

            foreach (var feeding in feedings)
            {
                FeedingCollection.Add(feeding);
            }
        }

        void InitializeSubscribes()
        {
            MessagingCenter.Subscribe<ChestFeedPage, Feeding>(this, "AddOrUpdateFeeding", async (sender, feeding) =>
            {
                if (feeding.Key == null)
                {
                    feeding.Key = Guid.NewGuid().ToString();
                    await Database.Insert(feeding);
                }
                else
                {
                    await Database.Update(feeding);
                }
            });

            MessagingCenter.Subscribe<ChestFeedPage, Feeding>(this, "DeleteFeeding", async (sender, feeding) =>
            {
                await Database.Delete(feeding);
            });

            MessagingCenter.Subscribe<FoodFeedPage, Feeding>(this, "AddOrUpdateFoodType", async (sender, feeding) =>
            {
                if (feeding.Key == null)
                {
                    feeding.Key = Guid.NewGuid().ToString();
                    await Database.Insert(feeding);
                }
                else
                {
                    await Database.Update(feeding);
                }
            });

            MessagingCenter.Subscribe<FoodFeedPage, Feeding>(this, "DeleteFoodType", async (sender, feeding) =>
            {
                await Database.Delete(feeding);
            });
        }

        public void LoadFoodTypes()
        {
            FoodTypeList = new List<KeyValuePair<int, string>>();

            var enumMembers = Enums
                .GetMembers(typeof(FeedByChestSide));

            foreach (var enumMember in enumMembers)
            {
                var keyValuePair = new KeyValuePair<int, string>((int)enumMember.Value, enumMember.Attributes.Get<DescriptionAttribute>().Description);
                FoodTypeList.Add(keyValuePair);
            }

            if (Feeding.FoodType == FoodType.Chest && Feeding.FoodDescription != null)
            {
                SelectedItem = FoodTypeList.FirstOrDefault(x => x.Value == Feeding.FoodDescription);
            }
        }
    }
}
