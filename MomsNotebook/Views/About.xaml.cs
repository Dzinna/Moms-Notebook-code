using Xamarin.Forms;

namespace MomsNotebook.Views
{
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}