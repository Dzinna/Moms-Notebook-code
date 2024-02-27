using MomsNotebook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views.Helpers.Marker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarkersViewPage : ContentPage
    {
        MarkersViewModel MarkersViewModel { get; set; }
        public MarkersViewPage()
        {
            InitializeComponent();

            MarkersViewModel = new MarkersViewModel();

            BindingContext = MarkersViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MarkersViewModel = new MarkersViewModel();

            BindingContext = MarkersViewModel;
        }

        private async void Plus_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MarkerPage());
        }
    }
}