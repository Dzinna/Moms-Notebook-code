using MomsNotebook.Services.Repositories;
using MomsNotebook.Utils;
using Xamarin.Forms;

namespace MomsNotebook.Views
{
    public partial class Login : ContentPage
    {   
        public Login()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(pass.Text) && !string.IsNullOrEmpty(email.Text))
            {
                GlobalProperties.UserEmail = email.Text.Trim().ToLower();

                if(GlobalProperties.IsEmailValid)
                {                  
                    var mysql = DependencyService.Get<IMySqlDatabase>();

                    if (mysql.Active && mysql.CheckLoginSuccess(email.Text, pass.Text))
                    {
                        await DisplayAlert("Prisijungimas.", "Jūs sėkmingai prisijungėte.", "Tęsti.");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Slaptažodis.", "Jūsų slaptažodis neteisingas.", "Tęsti.");
                    }
                }
                else
                {
                    await DisplayAlert("El.paštas nevalidus.", "Privalote užildyti el. paštą.", "Pildyti.");
                }
            }
            else
            {
                await DisplayAlert("El.paštas arba slaptažodis tušti.", "Privalote užildyti laukus.", "Pildyti.");
            }
        }

        private async void ImageButton_Clicked_Register(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new Register());
        }
    }
}