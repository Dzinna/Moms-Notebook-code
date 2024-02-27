using MomsNotebook.Models.Enums;
using System;
using MomsNotebook.Views.Helpers.Food;
using Xamarin.Forms;

namespace MomsNotebook.Views
{
    public partial class FoodPage : ContentPage
    {
        public FoodPage()
        {
            InitializeComponent();
        }

        async void Krutine_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ChestFeedViewPage(FoodType.Chest));
        }
        async void Misinukai_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new FoodViewPage(FoodType.InfantMilk));
        }
        async void Maitinimas_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new FoodViewPage(FoodType.OrganicFood));
        }
    }
}