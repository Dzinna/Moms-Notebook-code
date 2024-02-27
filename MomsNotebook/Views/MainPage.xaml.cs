using System;
using MomsNotebook.Views.Helpers.Marker;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {           
            InitializeComponent();
        }

        async void Kontaktai_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new Contacts());
        }
        async void Zymekliai_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MarkersViewPage());
        }
        async void Rutina_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DailyRoutine());
        }
        async void Duomenys_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Data());
        }
    }
}