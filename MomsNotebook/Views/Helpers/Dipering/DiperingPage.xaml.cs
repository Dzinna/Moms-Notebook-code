using MomsNotebook.Models.Database;
using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;

namespace MomsNotebook.Views.Helpers.Dipering
{
    public partial class DiperingPage : ContentPage
    {
        public DiperingViewModel DiperingViewModel { get; set; }
        public DiperingPage()
        {
            InitializeComponent();

            DiperingViewModel = new DiperingViewModel();
            BindingContext = DiperingViewModel;
        }

        async void Add_Clicked(object sender, EventArgs eventArgs)
        {
            await Navigation.PushAsync(new DiperingViewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            DiperingViewModel.PopulateListCommand.Execute(null);

            DiperingListView.ItemsSource = DiperingViewModel.DiperingsCollection;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var diper = DiperingListView.SelectedItem as Dipers;

            if (diper == null)
            {
                return;
            }

            DiperingViewModel.Diper = diper;

            await Navigation.PushAsync(new DiperingViewPage(DiperingViewModel));

            DiperingListView.SelectedItem = null;
        }
    }
}