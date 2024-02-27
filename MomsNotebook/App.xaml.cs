using MomsNotebook.Services;
using MomsNotebook.Services.ExitApp;
using MomsNotebook.Services.Ftp;
using MomsNotebook.Services.Repositories;
using MomsNotebook.Views;
using Xamarin.Forms;

namespace MomsNotebook
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ISqlLiteDatabase, SqlLiteDatabase>();
            DependencyService.Register<IMySqlDatabase, MySqlDatabase>();
            DependencyService.Register<IFtpSyncService, FtpSyncService>();
            DependencyService.Register<ICloseApplication, CloseApplication>();
            DependencyService.Register<ISynchronizationService, SynchronizationService>();

            MainPage = new RootPage();
        }

        protected override void OnStart()
        {            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
