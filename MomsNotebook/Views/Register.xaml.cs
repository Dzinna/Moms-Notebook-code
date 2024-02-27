using MomsNotebook.Services.Repositories;
using MomsNotebook.Utils;
using Xamarin.Forms;

namespace MomsNotebook.Views
{
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {          
            var mysql = DependencyService.Get<IMySqlDatabase>();

            GlobalProperties.UserEmail = email.Text.Trim().ToLower();

            if(!GlobalProperties.IsEmailValid)
            {
                await DisplayAlert("Elektroninis paštas.","El. paštas nevalidus.","Pildyti.");
                return;
            }

            if (!string.IsNullOrEmpty(pass.Text) && !string.IsNullOrEmpty(passRepeat.Text))
            {
                if (pass.Text != passRepeat.Text)
                {
                    await DisplayAlert("Slaptažodžiai.", "Slaptažodžiai privalo sutapti.", "Pildyti.");
                }
            }
            else
            {
                await DisplayAlert("Slaptažodžiai.", "Slaptažodžiai negali būti tušti.", "Pildyti.");
                return;
            }

            if (mysql.Active && !mysql.CheckEmailExistsInDatabase(email.Text))
            {
                mysql.CreateLogin(email.Text, pass.Text);                

                if(mysql.CheckLoginSuccess(email.Text, pass.Text))
                {
                    await DisplayAlert("Registracija.", "Prisiregistravote sėkmingai.", "Tęsti.");

                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await DisplayAlert("Elektroninis paštas.", "El. paštas egzistuoja, bandykite prisijungti.", "Tęsti.");
            }
        }

        private async void ImageButton_Clicked_Return(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}