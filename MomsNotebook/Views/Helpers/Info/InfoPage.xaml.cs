using MomsNotebook.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Info
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {        
        InfoViewModel InfoViewModel { get; }

        public InfoPage()
        {
            InitializeComponent();

            InfoViewModel = new InfoViewModel();

            BindingContext = InfoViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InfoViewModel.PopulateListCommand.Execute(null);
        }
    }
}