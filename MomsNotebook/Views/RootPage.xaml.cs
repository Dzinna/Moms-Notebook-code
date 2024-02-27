using MomsNotebook.Services;
using MomsNotebook.Services.Ftp;
using MomsNotebook.Services.Repositories;
using MomsNotebook.Utils;
using MomsNotebook.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MomsNotebook.Models.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MomsNotebook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : MasterDetailPage
    {
        private Command SynchronizationCommand { get; }
        private Command ConnectionsCommand { get; }
        public static ISqlLiteDatabase Sqlite => DependencyService.Get<ISqlLiteDatabase>();
        public static IMySqlDatabase MySql => DependencyService.Get<IMySqlDatabase>();
        public static IFtpSyncService FtpClient => DependencyService.Get<IFtpSyncService>();

        public RootPage()
        {
            InitializeComponent();

            Sqlite.CreateTables.Execute(null);

            ConnectionsCommand = new Command(async () =>
            {
                // Problems with using async methods for MySQL due to that running in separate Task
                await Task.Run(() => OpenMySqlConnection());
                await OpenFtpConnectionAsync();
                await DependencyService.Get<ISynchronizationService>().UpdateInfoTable();
            });

            ConnectionsCommand.Execute(null);

            SynchronizationCommand = new Command(async () => await ControlSynchronization());

            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RootPageMasterMenuItem;

            if (item == null)
            {
                return;
            }

            if (item.TargetType == typeof(Synchronize))
            {
                SynchronizationCommand.Execute(null);

                Detail = new NavigationPage(new MainPage());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;

                return;
            }

            if (item.TargetType == typeof(Login))
            {
                if (GlobalProperties.UserConnected)
                {
                    await DisplayAlert("Prisijungimas.", "Jūs jau prisijungęs.", "Tęsti.");

                    IsPresented = false;

                    MasterPage.ListView.SelectedItem = null;

                    return;
                }

                await Navigation.PushModalAsync(new Login());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;

                return;
            }

            if (item.TargetType == typeof(About))
            {
                await Navigation.PushModalAsync(new About());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;

                return;
            }

            if (item.TargetType == typeof(Exit))
            {
                await Navigation.PushAsync(new Exit());
                IsPresented = false;

                MasterPage.ListView.SelectedItem = null;

                return;
            }

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private async Task ControlSynchronization()
        {
            if (MySql.Active && !string.IsNullOrEmpty(GlobalProperties.UserEmail) && GlobalProperties.UserConnected)
            {
                await SynchronizeDatabase();

                await DisplayAlert(
                "Sinchronizacija.",
                "Jūsų sinchronizacija sėkminga.",
                "Uždaryti.");
            }
            else
            {
                await DisplayAlert(
                "Duomenų sinchronizacija.",
                "Turite būti prisijungę, kad galėtumėte sinchronizuoti.",
                "Uždaryti.");
            }
        }

        private async Task SynchronizeDatabase()
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
            return;
            if (FtpClient.Active)
            {
                if(await FtpClient.FileExistsAsync(GlobalProperties.Uuid))
                {
                    // Rename current database
                    var newFileNameToMerge = GlobalProperties.DatabasePath.Replace("Database", "Database_New");

                    // Copy current database file to new file name database
                    File.Copy(GlobalProperties.DatabasePath, newFileNameToMerge, true);

                    // After copy delete current database file, because copy was made and prepare for new file from
                    // synchronization server
                    File.Delete(GlobalProperties.DatabasePath);

                    // Download synchronize file from server
                    await FtpClient.DownloadFileAsync(GlobalProperties.Uuid);

                    await DependencyService.Get<ISynchronizationService>().Synchronize();
                }
                else
                {
                    // If file not exists it means no file under this logged in user were uploaded to synchronization server
                    await FtpClient.UploadFileAsync(GlobalProperties.Uuid);
                }
            }
        }

        private async Task OpenMySqlConnection()
        {
            try
            {
                MySql.GetMySqlConnection();
            }
            catch (Exception)
            {
                await DisplayAlert(
                "Sinchronizacija su MySQL negalima.",
                "Serveris nepasiekiamas.",
                "Uždaryti");
            }
        }

        private async Task OpenFtpConnectionAsync()
        {
            try
            {                
                await FtpClient.ConnectToFTPSAsync();
            }
            catch (Exception)
            {
                await DisplayAlert(
                "Sinchronizacija su FTPS negalima.",
                "Serveris nepasiekiamas.",
                "Uždaryti");
            }
        }
    }
}