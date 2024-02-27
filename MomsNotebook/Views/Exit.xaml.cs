using MomsNotebook.Services.ExitApp;
using MomsNotebook.Services.Ftp;
using MomsNotebook.Services.Repositories;
using Xamarin.Forms;

namespace MomsNotebook.Views
{
    public partial class Exit : ContentPage
    {
        public Exit()
        {
            InitializeComponent();

            // Memory clean up
            DependencyService.Get<IMySqlDatabase>().Dispose();

            DependencyService.Get<IFtpSyncService>().Dispose();

            var exit = DependencyService.Get<ICloseApplication>();
            exit.ExitApp();
        }
    }
}