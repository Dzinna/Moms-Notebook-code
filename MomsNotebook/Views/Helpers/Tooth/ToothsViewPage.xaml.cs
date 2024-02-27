using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Tooth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToothsViewPage : ContentPage
    {
        ToothsViewModel ToothsViewModel { get; set; }

        public ToothsViewPage()
        {
            InitializeComponent();

            ToothsViewModel = new ToothsViewModel();

            BindingContext = ToothsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ToothsViewModel.InitializeCollection.Execute(null);

            ToothsListView.ItemsSource = ToothsViewModel.ToothsCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var tooth = ToothsListView.SelectedItem as Tooths;

            if (tooth == null)
            {
                return;
            }

            ToothsViewModel.Tooth = tooth;

            await Navigation.PushAsync(new ToothsPage(ToothsViewModel));

            ToothsListView.SelectedItem = null;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new ToothsPage());
        }
    }
}